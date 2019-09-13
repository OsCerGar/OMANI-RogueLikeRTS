using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GamemasterController : MonoBehaviour
{
    public static GamemasterController GameMaster;

    bool developer;
    public int Money, Difficulty;

    #region Robots
    private List<Robot.RobotData> savedRobots = new List<Robot.RobotData>();
    #region specificRobotValues
    //Worker
    public int WorkerDamageLevel;

    //Swordman
    public int SwordmanDamageLevel;

    //Shooter 
    public int ShooterDamageLevel;
    #endregion

    #region genericRobotValues

    public int RobotQuantityLevel;
    public int RobotMaxLifeLevel;
    public int RobotLoadSpeedLevel;
    public int RobotCriticalChanceLevel;
    public int RobotBaseDamageLevel;
    public int RobotExpLevel;
    public int RobotControlRadLevel;

    #endregion

    #endregion
    #region Flags
    public int FlagQuantityLevel;
    public int FlagCriticalZoneLevel;
    public int FlagCriticalZoneDamage;
    public int FlagDurationLevel;
    public int FlagReloadSpeedLevel;
    public int FlagRadLevel;

    #endregion
    #region Laser
    public int LaserSelectedLevel;
    #region LaserSpecificLevels


    #endregion

    public int LaserDamageLevel;
    public int LaserMaxDistanceLevel;
    public int LaserTransmisionLevel;
    #endregion
    #region Armor

    public int MaxLifeLevel;
    public int MaxSpeedLevel;

    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        if (GameMaster == null)
        {
            GameMaster = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (GameMaster != this)
        {
            Destroy(gameObject);
        }
        Debug.Log(Application.persistentDataPath);
        //Load();
    }

    public void AddRobot(Robot _robot)
    {
        if (savedRobots != null)
        {
            if (!savedRobots.Contains(_robot.data))
            {
                savedRobots.Add(_robot.data);
                Save();
            }
        }
    }

    public void RemoveRobot(Robot _robot)
    {
        if (savedRobots != null)
        {
            savedRobots.Remove(_robot.data);
            Save();
        }
    }

    public List<Robot.RobotData> GetRobots()
    {
        return savedRobots;
    }

    public void Save()
    {
        if (developer != true)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/omaniSaveData.dat");

            GameData data = new GameData();

            //Money
            data.Money = Money;
            data.Difficulty = Difficulty;

            #region Robots
            data.savedRobots = savedRobots;
            #region specificRobotValues
            //Worker
            data.WorkerDamageLevel = WorkerDamageLevel;

            //Swordman
            data.SwordmanDamageLevel = SwordmanDamageLevel;

            //Shooter 
            data.ShooterDamageLevel = ShooterDamageLevel;
            #endregion

            #region genericRobotValues

            data.RobotQuantityLevel = RobotQuantityLevel;
            data.RobotMaxLifeLevel = RobotMaxLifeLevel;
            data.RobotLoadSpeedLevel = RobotLoadSpeedLevel;
            data.RobotCriticalChanceLevel = RobotCriticalChanceLevel;
            data.RobotBaseDamageLevel = RobotBaseDamageLevel;
            data.RobotExpLevel = RobotExpLevel;
            data.RobotControlRadLevel = RobotControlRadLevel;

            #endregion
            #endregion

            #region Flags
            data.FlagQuantityLevel = FlagQuantityLevel;
            data.FlagCriticalZoneLevel = FlagCriticalZoneLevel;
            data.FlagCriticalZoneDamage = FlagCriticalZoneDamage;
            data.FlagDurationLevel = FlagDurationLevel;
            data.FlagReloadSpeedLevel = FlagReloadSpeedLevel;
            data.FlagRadLevel = FlagRadLevel;

            #endregion

            #region Laser
            data.LaserSelectedLevel = LaserSelectedLevel;
            #region LaserSpecificLevels


            #endregion

            data.LaserDamageLevel = LaserDamageLevel;
            data.LaserMaxDistanceLevel = LaserMaxDistanceLevel;
            data.LaserTransmisionLevel = LaserTransmisionLevel;
            #endregion

            #region Armor

            data.MaxLifeLevel = MaxLifeLevel;
            data.MaxSpeedLevel = MaxSpeedLevel;

            #endregion

            bf.Serialize(file, data);
            file.Close();
        }
    }

    //Creates and loads a developer save game
    public void Developer()
    {
        developer = true;

        //Money
        Money = 99999;

        #region specificRobotValues
        //Worker
        WorkerDamageLevel = 0;

        //Swordman
        SwordmanDamageLevel = 0;

        //Shooter 
        ShooterDamageLevel = 0;
        #endregion

        #region genericRobotValues

        RobotQuantityLevel = 0;
        RobotMaxLifeLevel = 0;
        RobotLoadSpeedLevel = 0;
        RobotCriticalChanceLevel = 0;
        RobotBaseDamageLevel = 0;
        RobotExpLevel = 0;
        RobotControlRadLevel = 0;

        #endregion

        #region Flags
        FlagQuantityLevel = 0;
        FlagCriticalZoneLevel = 0;
        FlagCriticalZoneDamage = 0;
        FlagDurationLevel = 0;
        FlagReloadSpeedLevel = 0;
        FlagRadLevel = 0;

        #endregion

        #region Laser
        LaserSelectedLevel = 0;
        #region LaserSpecificLevels


        #endregion

        LaserDamageLevel = 0;
        LaserMaxDistanceLevel = 0;
        LaserTransmisionLevel = 0;
        #endregion

        #region Armor

        MaxLifeLevel = 0;
        MaxSpeedLevel = 0;

        #endregion

    }
    public void Load()
    {
        if (developer != true)
        {
            if (File.Exists(Application.persistentDataPath + "/omaniSaveData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/omaniSaveData.dat", FileMode.Open);

                GameData data = (GameData)bf.Deserialize(file);
                file.Close();
                //Money
                Money = data.Money;
                Difficulty = data.Difficulty;

                #region Robots
                savedRobots = data.savedRobots;
                #region specificRobotValues
                //Worker
                WorkerDamageLevel = data.WorkerDamageLevel;

                //Swordman
                SwordmanDamageLevel = data.SwordmanDamageLevel;

                //Shooter 
                ShooterDamageLevel = data.ShooterDamageLevel;
                #endregion

                #region genericRobotValues

                RobotQuantityLevel = data.RobotQuantityLevel;
                RobotMaxLifeLevel = data.RobotMaxLifeLevel;
                RobotLoadSpeedLevel = data.RobotLoadSpeedLevel;
                RobotCriticalChanceLevel = data.RobotCriticalChanceLevel;
                RobotBaseDamageLevel = data.RobotBaseDamageLevel;
                RobotExpLevel = data.RobotExpLevel;
                RobotControlRadLevel = data.RobotControlRadLevel;

                #endregion
                #endregion

                #region Flags
                FlagQuantityLevel = data.FlagQuantityLevel;
                FlagCriticalZoneLevel = data.FlagCriticalZoneLevel;
                FlagCriticalZoneDamage = data.FlagCriticalZoneDamage;
                FlagDurationLevel = data.FlagDurationLevel;
                FlagReloadSpeedLevel = data.FlagReloadSpeedLevel;
                FlagRadLevel = data.FlagRadLevel;

                #endregion

                #region Laser
                LaserSelectedLevel = data.LaserSelectedLevel;
                #region LaserSpecificLevels


                #endregion

                LaserDamageLevel = data.LaserDamageLevel;
                LaserMaxDistanceLevel = data.LaserMaxDistanceLevel;
                LaserTransmisionLevel = data.LaserTransmisionLevel;
                #endregion

                #region Armor

                MaxLifeLevel = data.MaxLifeLevel;
                MaxSpeedLevel = data.MaxSpeedLevel;

                #endregion
            }
        }
    }
    public void AddMoney(int _moneyToAdd)
    {
        Money += _moneyToAdd;
    }

    public string[] getCsvValues(String _statName, int _level)
    {
        using (var parser = new StreamReader(@"Assets\GameData.csv"))
        {
            while (!parser.EndOfStream)
            {
                var line = parser.ReadLine();
                var values = line.Split(',');
                try
                {
                    if (values[0].Equals(_statName) && int.Parse(values[1]) == _level)
                    {
                        return values;
                    }
                }
                catch (FormatException)
                {
                    Debug.Log("Reading csv, string where a int is supossed to be");
                }
            }
        }

        return null;

    }

    public string[] getCsvValues(String _statName)
    {
        using (var parser = new StreamReader(@"Assets\GameData.csv"))
        {
            while (!parser.EndOfStream)
            {
                var line = parser.ReadLine();
                var values = line.Split(',');
                try
                {
                    if (values[0].Equals(_statName))
                    {
                        return values;
                    }
                }
                catch (FormatException)
                {
                    Debug.Log("Reading csv, string where a int is supossed to be");
                }
            }
        }

        return null;

    }
    [Serializable]
    class GameData
    {
        public int Money;
        public int Difficulty;

        #region Robots
        public List<Robot.RobotData> savedRobots = new List<Robot.RobotData>();
        #region specificRobotValues
        //Worker
        public int WorkerDamageLevel;

        //Swordman
        public int SwordmanDamageLevel;

        //Shooter 
        public int ShooterDamageLevel;
        #endregion

        #region genericRobotValues

        public int RobotQuantityLevel;
        public int RobotMaxLifeLevel;
        public int RobotLoadSpeedLevel;
        public int RobotCriticalChanceLevel;
        public int RobotBaseDamageLevel;
        public int RobotExpLevel;
        public int RobotControlRadLevel;

        #endregion
        #endregion

        #region Flags
        public int FlagQuantityLevel;
        public int FlagCriticalZoneLevel;
        public int FlagCriticalZoneDamage;
        public int FlagDurationLevel;
        public int FlagReloadSpeedLevel;
        public int FlagRadLevel;

        #endregion

        #region Laser
        public int LaserSelectedLevel;
        #region LaserSpecificLevels


        #endregion

        public int LaserDamageLevel;
        public int LaserMaxDistanceLevel;
        public int LaserTransmisionLevel;
        #endregion

        #region Armor

        public int MaxLifeLevel;
        public int MaxSpeedLevel;

        #endregion
    }


}
