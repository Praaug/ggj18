using UnityEngine;
using System.Collections;
using Databases;
using System.Collections.Generic;
using System.Text;

public static class DatabaseManager
{
	//public static void SendCreateSessionCommand(SaveGame saveGame)
	//{
	//	RestManager.instance.PUT("sessions/create", JsonUtility.ToJson(saveGame, false), null);
	//}

	public static void SendUpdateSessionCommand(SaveGame saveGame)
	{
		RestManager.instance.PUT("sessions/update", JsonUtility.ToJson(saveGame, false), null);
	}

	public static void SendRemoveSessionCommand(SaveGame saveGame)
	{
		RestManager.instance.PUT("sessions/remove", JsonUtility.ToJson(saveGame, false), null);
	}

	public static void SendGetSessionsCommand(System.Action<string> onComplete)
	{
		RestManager.instance.GET("sessions", onComplete);
	}
}
