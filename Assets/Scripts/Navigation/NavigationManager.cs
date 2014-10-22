using System.Collections.Generic;
using UnityEngine;
public static class NavigationManager {

	public struct Route
	{
		public string RouteDescription;
		public bool CanTravel;
	}

	public static Dictionary<string, Route> RouteInformation = new Dictionary<string, Route>() {
		{ "Battle", new Route { CanTravel = true}},
		{ "World", new Route { RouteDescription = "The big bad world", CanTravel = true}},
		{ "Cave", new Route {RouteDescription = "The deep dark cave", CanTravel = false}},
		{ "Home", new Route { RouteDescription = "Home sweet home", CanTravel = true}},
		{ "Kirkidw", new Route {RouteDescription = "The grand city of Kirkidw", CanTravel = true}},
		{ "Shop", new Route {CanTravel = true}},
	};

	private static string PreviousLocation;

	public static string GetRouteInfo(string destination)
	{
		return RouteInformation.ContainsKey(destination) ? RouteInformation[destination].RouteDescription : null;
	}

	public static bool CanNavigate(string destination)
	{
		return RouteInformation.ContainsKey(destination) ? RouteInformation[destination].CanTravel : false;
	}

	public static void NavigateTo(string destination)
	{
		PreviousLocation = Application.loadedLevelName;
		if (destination == "Home")
		{
			GameState.playerReturningHome = false;
		}
		GameState.SaveState();
		FadeInOutManager.FadeToLevel(destination); 
	}

	public static void GoBack()
	{
		var backlocation = PreviousLocation;
		PreviousLocation = Application.loadedLevelName;
		GameState.SaveState();
		FadeInOutManager.FadeToLevel(backlocation); 
	}
}
