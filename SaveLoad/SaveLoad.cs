using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad
{
    private static string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/highscore.abc";
        }
    }

    public static void SavePlayer(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            PlayerData loadedData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return loadedData;
        }
        else
        {
            SavePlayer(PlayerData.DefaultValues);

            return PlayerData.DefaultValues;
        }
    }
}
public class SaveLoadCustomize
{
    private static string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/customize.abc";
        }
    }

    public static void SavePlayer(PlayerDataCustomize data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataCustomize LoadPlayer()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            PlayerDataCustomize loadedData = formatter.Deserialize(stream) as PlayerDataCustomize;
            stream.Close();

            return loadedData;
        }
        else
        {
            SavePlayer(PlayerDataCustomize.DefaultValues);

            return PlayerDataCustomize.DefaultValues;
        }
    }
}

public class SaveLoadCoins
{
    private static string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/coins.abc";
        }
    }

    public static void SavePlayer(PlayerDataCoins data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataCoins LoadPlayer()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            PlayerDataCoins loadedData = formatter.Deserialize(stream) as PlayerDataCoins;
            stream.Close();

            return loadedData;
        }
        else
        {
            SavePlayer(PlayerDataCoins.DefaultValues);

            return PlayerDataCoins.DefaultValues;
        }
    }
}

public class SaveLoadOrbs
{
    private static string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/orbs.abc";
        }
    }

    public static void SavePlayer(PlayerDataOrbs data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataOrbs LoadPlayer()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            PlayerDataOrbs loadedData = formatter.Deserialize(stream) as PlayerDataOrbs;
            stream.Close();

            return loadedData;
        }
        else
        {
            SavePlayer(PlayerDataOrbs.DefaultValues);

            return PlayerDataOrbs.DefaultValues;
        }
    }
}

public class SaveLoadLocked
{
    private static string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/locked.abc";
        }
    }

    public static void SavePlayer(PlayerDataLocked data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataLocked LoadPlayer()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            PlayerDataLocked loadedData = formatter.Deserialize(stream) as PlayerDataLocked;
            stream.Close();

            return loadedData;
        }
        else
        {
            SavePlayer(PlayerDataLocked.DefaultValues);

            return PlayerDataLocked.DefaultValues;
        }
    }
}

public class SaveLoadSettings
{
    private static string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/settings.abc";
        }
    }

    public static void SavePlayer(PlayerDataSettings data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataSettings LoadPlayer()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            PlayerDataSettings loadedData = formatter.Deserialize(stream) as PlayerDataSettings;
            stream.Close();

            return loadedData;
        }
        else
        {
            SavePlayer(PlayerDataSettings.DefaultValues);

            return PlayerDataSettings.DefaultValues;
        }
    }
}
public class SaveLoadUpgrade
{
    private static string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/upgrades.abc";
        }
    }

    public static void SavePlayer(PlayerDataUpgrade data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerDataUpgrade LoadPlayer()
    {
        if (File.Exists(FilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(FilePath, FileMode.Open);

            PlayerDataUpgrade loadedData = formatter.Deserialize(stream) as PlayerDataUpgrade;
            stream.Close();

            return loadedData;
        }
        else
        {
            SavePlayer(PlayerDataUpgrade.DefaultValues);

            return PlayerDataUpgrade.DefaultValues;
        }
    }
}