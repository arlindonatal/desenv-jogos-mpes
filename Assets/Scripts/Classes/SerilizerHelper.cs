using System.IO;
using System.Xml.Serialization;

public class SerializerHelper {

    public static byte[] Serialise<T>(T input)
    {
        byte[] output = null;
        //Create an XML formatter
        var serializer = new XmlSerializer(typeof(T));
        try
        {
            //Create an in memory stream to hold our serialised output
            using (var stream = new MemoryStream())
            {
                //Serialise the data
                serializer.Serialize(stream, input);
                //Get the serialised output
                output = stream.ToArray();
            }
        }
        catch { }

        //Return the serialized output
        return output;
    }

    public static T DeSerialise<T>(Stream input)
    {
        T output = default(T);
        //Create an XML formatter
        var serializer = new XmlSerializer(typeof(T));
        try
        {
            //Deserialise the data from the stream
            output = (T)serializer.Deserialize(input);
        }
        catch { }
        //Return the deserialized output
        return output;
    }

    public static T DeSerialise<T>(byte[] input)
    {
        T output = default(T);
        //Create an XML formatter
        var serializer = new XmlSerializer(typeof(T));
        try
        {
            //Create an in memory stream with the serialsed data in it
            using (var stream = new MemoryStream(input))
            {
                //Deserialise the data from the stream
                output = (T)serializer.Deserialize(stream);
            }
        }
        catch { }

        //Return the deserialized output
        return output;
    }

}
