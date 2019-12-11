using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Equipment{















    public class ExperimentalEquipmentGenerator
    {
        private class EquipmentPrototype
        {
            public int id = 0;
            public string iconName = "";
            public string name = "";
            public string description = "";
            public float mass = 0f;
            public Equipment.Grade grade = Equipment.Grade.Amateurish;
            public Equipment.State state = Equipment.State.TurnedOff;
            public List<Equipment.Type> types = new List<Equipment.Type>();
            public bool requireTarget = false;
            public bool canBeTurnOn = false;
            public bool canBeActivated = false;
            public bool canBeOverdrive = false;
            public float poweringUpTime = 0f;
            public float poweringDownTime = 0f;
            public float durabilityMax = 0f;
            public float heatMax = 0f;

            private class EquipmentPropertyPrototype
            {
                public int id = 0;
                public PropertyName name = PropertyName.None;
                public float valueOnTurnOff = 0f;
                public float valueOnTurnOn = 0f;
                public float valueOnActive = 0f;
                public float valueOnOverdrive = 0f;
                public float valueOnOverheated = 0f;
                public float valueOnPoweringUp = 0f;
                public float valueOnPoweringDown = 0f;
            }

            private List<EquipmentPropertyPrototype> propertyList = new List<EquipmentPropertyPrototype>();

            public void addProperty(PropertyName _name, float _valueOnTurnOff, float _valueOnTurnOn, float _valueOnActive, float _valueOnOverdrive, float _valueOnOverheated, float _valueOnPoweringUp, float _valueOnPoweringDown)
            {
                EquipmentPropertyPrototype newProperty = new EquipmentPropertyPrototype();
                newProperty.id = id;
                newProperty.name = _name;
                newProperty.valueOnTurnOff = _valueOnTurnOff;
                newProperty.valueOnTurnOn = _valueOnTurnOn;
                newProperty.valueOnActive = _valueOnActive;
                newProperty.valueOnOverdrive = _valueOnOverdrive;
                newProperty.valueOnOverheated = _valueOnOverheated;
                newProperty.valueOnPoweringUp = _valueOnPoweringUp;
                newProperty.valueOnPoweringDown = _valueOnPoweringDown;
                propertyList.Add(newProperty);
            }

            public Equipment getEquipment()
            {
                Equipment eq = new Equipment();

                eq.id = this.id;
                eq.iconName = this.iconName;
                eq.name = this.name;
                eq.description = this.description;
                eq.mass = this.mass;
                eq.grade = this.grade;
                eq.state = State.TurnedOff;
                eq.types = new List<Equipment.Type>(types.ToArray());
                eq.requireTarget = this.requireTarget;
                eq.canBeTurnOn = this.canBeTurnOn;
                eq.canBeActivated = this.canBeActivated;
                eq.canBeOverdrive = this.canBeOverdrive;
                eq.poweringUpTime = this.poweringUpTime;
                eq.poweringDownTime = this.poweringDownTime;
                eq.durabilityMax = this.durabilityMax;
                eq.heatMax = this.heatMax;

                for (int i = 0; i < propertyList.Count; i++)
                {
                    eq.addPropoerty(propertyList[i].name, propertyList[i].valueOnTurnOff, propertyList[i].valueOnTurnOn, propertyList[i].valueOnActive, propertyList[i].valueOnOverdrive, propertyList[i].valueOnOverheated, propertyList[i].valueOnPoweringUp, propertyList[i].valueOnPoweringDown);
                }

                return eq;
            }
        }

        private List<EquipmentPrototype> equipmentPrototypeList = new List<EquipmentPrototype>();

        public ExperimentalEquipmentGenerator()
        {
            EquipmentPrototype equipmentPrototype;

            //id;iconName;name;description;types;mass;grade;requireTarget;canBeTurnOn;canBeActivated;canBeOverdrive;poweringUpTime;poweringDownTime;durabilityMax;heatMax;
            equipmentPrototype = new EquipmentPrototype();
            equipmentPrototype.               id = 0;
            equipmentPrototype.         iconName = "";
            equipmentPrototype.             name = "";
            equipmentPrototype.      description = "";
            equipmentPrototype.             mass = 0f;
            equipmentPrototype.            grade = Equipment.Grade.Amateurish;
            equipmentPrototype.            types = typesList(Equipment.Type.ShipCore);
            equipmentPrototype.    requireTarget = false;
            equipmentPrototype.      canBeTurnOn = false;
            equipmentPrototype.   canBeActivated = false;
            equipmentPrototype.   canBeOverdrive = false;
            equipmentPrototype.   poweringUpTime = 0f;
            equipmentPrototype. poweringDownTime = 0f;
            equipmentPrototype.    durabilityMax = 0f;
            equipmentPrototype.          heatMax = 0f;
            //propertyName;valueOnTurnOff;valueOnTurnOn;valueOnActive;valueOnOverdrive;valueOnOverheated;valueOnPoweringUp;valueOnPoweringDown;
            equipmentPrototype.addProperty(PropertyName.None, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
            equipmentPrototypeList.Add(equipmentPrototype);

            //id;iconName;name;description;types;mass;grade;requireTarget;canBeTurnOn;canBeActivated;canBeOverdrive;poweringUpTime;poweringDownTime;durabilityMax;heatMax;
            equipmentPrototype = new EquipmentPrototype();
            equipmentPrototype.               id = 1;
            equipmentPrototype.         iconName = "2_64_11";
            equipmentPrototype.             name = "Ship Core";
            equipmentPrototype.      description = "Core of the ship with basic defenses";
            equipmentPrototype.             mass = 5000f;
            equipmentPrototype.            grade = Equipment.Grade.Technocratic;
            equipmentPrototype.            types = typesList(Equipment.Type.ShipCore);
            equipmentPrototype.    requireTarget = false;
            equipmentPrototype.      canBeTurnOn = false;
            equipmentPrototype.   canBeActivated = false;
            equipmentPrototype.   canBeOverdrive = false;
            equipmentPrototype.   poweringUpTime = 0f;
            equipmentPrototype. poweringDownTime = 0f;
            equipmentPrototype.    durabilityMax = 0f;
            equipmentPrototype.          heatMax = 0f;
            //propertyName;valueOnTurnOff;valueOnTurnOn;valueOnActive;valueOnOverdrive;valueOnOverheated;valueOnPoweringUp;valueOnPoweringDown;
            equipmentPrototype.addProperty(PropertyName.MaxEnergy, 500f, 0f, 0f, 0f, 0f, 0f, 0f);
            equipmentPrototype.addProperty(PropertyName.EnergyProduction, 100f, 0f, 0f, 0f, 0f, 0f, 0f);
            equipmentPrototype.addProperty(PropertyName.MaxShield, 500f, 0f, 0f, 0f, 0f, 0f, 0f);
            equipmentPrototype.addProperty(PropertyName.MaxArmor, 500f, 0f, 0f, 0f, 0f, 0f, 0f);
            equipmentPrototype.addProperty(PropertyName.MaxHull, 1500f, 0f, 0f, 0f, 0f, 0f, 0f);
            equipmentPrototype.addProperty(PropertyName.ShieldRepair, 1f, 0f, 0f, 0f, 0f, 0f, 0f);
            equipmentPrototype.addProperty(PropertyName.ArmorRepair, 2f, 0f, 0f, 0f, 0f, 0f, 0f);
            equipmentPrototype.addProperty(PropertyName.HullRepair, 3f, 0f, 0f, 0f, 0f, 0f, 0f);
            equipmentPrototype.addProperty(PropertyName.TargetingRange, 15000f, 0f, 0f, 0f, 0f, 0f, 0f);
            equipmentPrototype.addProperty(PropertyName.NumberOfTargets, 2f, 0f, 0f, 0f, 0f, 0f, 0f);
            equipmentPrototype.addProperty(PropertyName.Drag, 1f, 0f, 0f, 0f, 0f, 0f, 0f);
            equipmentPrototype.addProperty(PropertyName.DragAngular, 1f, 0f, 0f, 0f, 0f, 0f, 0f);
            equipmentPrototypeList.Add(equipmentPrototype);

            //id;iconName;name;description;types;mass;grade;requireTarget;canBeTurnOn;canBeActivated;canBeOverdrive;poweringUpTime;poweringDownTime;durabilityMax;heatMax;
            equipmentPrototype = new EquipmentPrototype();
            equipmentPrototype.id = 2;
            equipmentPrototype.iconName = "1_64_1";
            equipmentPrototype.name = "Nuclear Generator";
            equipmentPrototype.description = "Energy generator that creates power.";
            equipmentPrototype.mass = 1000f;
            equipmentPrototype.grade = Equipment.Grade.Amateurish;
            equipmentPrototype.types = typesList(Equipment.Type.Energy, Equipment.Type.EnergyGenerator);
            equipmentPrototype.requireTarget = false;
            equipmentPrototype.canBeTurnOn = true;
            equipmentPrototype.canBeActivated = false;
            equipmentPrototype.canBeOverdrive = true;
            equipmentPrototype.poweringUpTime = 2f;
            equipmentPrototype.poweringDownTime = 2f;
            equipmentPrototype.durabilityMax = 0f;
            equipmentPrototype.heatMax = 0f;
            //propertyName;valueOnTurnOff;valueOnTurnOn;valueOnActive;valueOnOverdrive;valueOnOverheated;valueOnPoweringUp;valueOnPoweringDown;
            equipmentPrototype.addProperty(PropertyName.EnergyProduction, 0f, 100f, 0f, 150f, 0f, 0f, 0f);
            equipmentPrototypeList.Add(equipmentPrototype);





            /*
                3;1_64_7;Standard Battery;Battery that holds power;Energy, EnergyBattery;500;50;Amateurish;FALSE;TRUE;FALSE;FALSE;2;5;50;50;
                4;3_64_2;Fuel Engine Booster;Boost your engine;Mobility, Engine;500;50;Amateurish;FALSE;TRUE;FALSE;TRUE;2;5;50;50;
                5;1_64_15;Shield Generator;System that creates barrier around the ship.;Defence, Endurance, ShieldGenerator;500;50;Amateurish;FALSE;TRUE;FALSE;TRUE;2;5;100;50;
                6;2_64_3;Shield Booster;System for supporting shield regeneration;Defence, RepairSystem, ShieldRepair;50;50;Amateurish;FALSE;TRUE;TRUE;TRUE;2;5;50;50;
                7;2_64_2;Armor Reinforcer;System for supporting armor reinforcement;Defence, RepairSystem, ArmorRepair;50;50;Amateurish;FALSE;TRUE;TRUE;TRUE;2;5;50;50;
                8;2_64_4;Hull Repairer;System for supporting hull repairs;Defence, RepairSystem, HullRepair;50;50;Amateurish;FALSE;TRUE;TRUE;TRUE;2;5;50;50;
                9;12_64_16;Missile Launcher;Basic missile launcher;Weapon, MissileLauncher;500;10;Amateurish;TRUE;TRUE;TRUE;TRUE;2;5;100;50;
                10;5_64_17;Agility Plates;Wings that increase ability to maneuver;Mobility, ManeuverabilityEnchancer;100;50;Amateurish;FALSE;FALSE;FALSE;FALSE;0;0;0;0;
                11;3_64_9;Targetting Antena;Can see targets further;Targetting, TargetRange, TargetNumber;100;50;Amateurish;FALSE;TRUE;FALSE;FALSE;5;5;0;0;
                12;3_64_9;Energy Generator;Drain your shield to produce energy;Energy, EnergyGenerator;100;50;Amateurish;FALSE;TRUE;FALSE;FALSE;2;2;50;50;
             */

            /*


                3;MaxEnergy;0;500;500;500;0;0;0;

                4;EnergyConsumption;0;10;0;20;0;0;0;
                4;EngineThrust;0;3000000;0;7000000;0;0;0;
                4;DurabilityLoss;0;0.1;0;0.3;0;0;0;
                4;HeatProduction;-5;0.1;0;3;-4;-2;-2;

                5;MaxShield;0;200;0;250;0;0;0;
                5;EnergyConsumption;0;1;0;2;0;0;0;

                6;EnergyConsumption;0;10;20;30;0;0;0;
                6;ShieldRepair;0;0;10;15;0;0;0;

                7;EnergyConsumption;0;10;20;30;0;0;0;
                7;ArmorRepair;0;0;10;15;0;0;0;

                8;EnergyConsumption;0;10;20;30;0;0;0;
                8;HullRepair;0;0;10;15;0;0;0;
                8;MaxHull;100;100;100;100;100;0;0;

                9;EnergyConsumption;0;5;25;35;0;15;10;
                9;EnergyLocalBatteryCapacity;0;100;100;100;0;0;0;
                9;EnergyLocalBatteryChargeAmount;0;20;25;35;0;0;0;
                9;DurabilityLoss;0;0.1;0.2;0.3;0;0;0;
                9;HeatProduction;-5;0.1;1;3;-4;-2;-2;

                10;Maneuverability;7.5;0;0;0;0;0;0;

                11;EnergyConsumption;0;25;0;0;0;0;0;
                11;TargetingRange;0;15000;0;0;0;0;0;
                11;NumberOfTargets;0;1;0;0;0;0;0;

                12;EnergyProduction;0;5;0;8;0;0;0;
                12;ShieldRepair;0;-1;0;-2;0;0;0;
                12;DurabilityLoss;0;0.1;0;0.3;0;0;0;
                12;HeatProduction;-5;0.1;0;3;-4;-5;-5;
             */

        }

        private static List<Equipment.Type> typesList(params Equipment.Type[] types)
        {
            List<Equipment.Type> list = new List<Equipment.Type>();
            for(int i = 0; i < types.Length; i++)
            {
                list.Add(types[i]);
            }
            return list;
        }



    }
























    public class EquipmentDatabase{

		private class EquipmentPrototype{
			public int id = 0;
			public string iconName = "";
			public string name = "";
			public string description = "";
			public float mass = 0f;
			public Equipment.Grade grade = Equipment.Grade.Amateurish;
			public Equipment.State state = Equipment.State.TurnedOff;
            public List<Equipment.Type> types = new List<Equipment.Type>();
            public bool requireTarget = false;
			public bool canBeTurnOn = false;
			public bool canBeActivated = false;
			public bool canBeOverdrive = false;
            public float poweringUpTime = 0f;
            public float poweringDownTime = 0f;
            public float durabilityMax = 0f;
			public float heatMax = 0f;
		}

		private class EquipmentPropertyPrototype{
			public int id = 0;
			public PropertyName name = PropertyName.None;
			public float valueOnTurnOff = 0f;
			public float valueOnTurnOn = 0f;
			public float valueOnActive = 0f;
			public float valueOnOverdrive = 0f;
            public float valueOnOverheated = 0f;
            public float valueOnPoweringUp = 0f;
            public float valueOnPoweringDown = 0f;
            public string print(){ return id+", "+name+", "+valueOnTurnOff+", "+valueOnTurnOn+", "+valueOnActive+", "+valueOnOverdrive + ", " + valueOnOverheated + ", " + valueOnPoweringUp + ", " + valueOnPoweringDown; }
		}

		private List<EquipmentPrototype> equipmentPrototypes;
		private List<EquipmentPropertyPrototype> equipmentPropertyPrototypes;

		public EquipmentDatabase(){
			equipmentPrototypes = new List<EquipmentPrototype> ();
			equipmentPropertyPrototypes = new List<EquipmentPropertyPrototype> ();
			getEquipmentDatabase ();
			getEquipmentPropertyDatabase ();
		}

		private void getEquipmentDatabase(){
			StreamReader reader = new StreamReader ("Assets\\Scripts\\Equipment\\ItemDatabase.csv");
			reader.ReadLine ();

			string str = "";
			while(true){
				str = reader.ReadLine ();
				if (str!=null) {
					if(str.Trim().Length>0){
                        string[] typesStr;
						string[] values = str.Split (";"[0]);
						EquipmentPrototype equipmentPrototype = new EquipmentPrototype ();
						int index = 0;

						equipmentPrototype.id = System.Int32.Parse (values[index]); index++;
						equipmentPrototype.iconName = values[index]; index++;
						equipmentPrototype.name = values[index]; index++;
                        equipmentPrototype.description = values[index]; index++;
                        typesStr = values[index].Split(','); ; index++;
                        float.TryParse(values[index],out equipmentPrototype.mass); index++;

						if(values[index].Equals("Amateurish")) equipmentPrototype.grade = Equipment.Grade.Amateurish;
						if(values[index].Equals("Civil")) equipmentPrototype.grade = Equipment.Grade.Civil;
						if(values[index].Equals("Proffesional")) equipmentPrototype.grade = Equipment.Grade.Proffesional;
						if(values[index].Equals("Military")) equipmentPrototype.grade = Equipment.Grade.Military;
						if(values[index].Equals("Technocratic")) equipmentPrototype.grade = Equipment.Grade.Technocratic;
						index++;

						if(values[index].ToLower().Equals("true")) equipmentPrototype.requireTarget = true;
						if(values[index].ToLower().Equals("false")) equipmentPrototype.requireTarget = false;
						index++;

						if(values[index].ToLower().Equals("true")) equipmentPrototype.canBeTurnOn = true;
						if(values[index].ToLower().Equals("false")) equipmentPrototype.canBeTurnOn = false;
						index++;

						if(values[index].ToLower().Equals("true")) equipmentPrototype.canBeActivated = true;
						if(values[index].ToLower().Equals("false")) equipmentPrototype.canBeActivated = false;
						index++;

                        if (values[index].ToLower().Equals("true")) equipmentPrototype.canBeOverdrive = true;
                        if (values[index].ToLower().Equals("false")) equipmentPrototype.canBeOverdrive = false;
                        index++;

                        float.TryParse(values[index], out equipmentPrototype.poweringUpTime); index++;
                        float.TryParse(values[index], out equipmentPrototype.poweringDownTime); index++;
                        float.TryParse(values[index], out equipmentPrototype.durabilityMax); index++;
                        float.TryParse(values[index], out equipmentPrototype.heatMax); index++;

                        Equipment.Type eqType;
                        for (int i=0;i< typesStr.Length;i++)
                        {
                            try
                            {
                                eqType = (Equipment.Type)System.Enum.Parse(typeof(Equipment.Type), typesStr[i].Trim());
                                equipmentPrototype.types.Add(eqType);
                            }
                            catch
                            {
                                Debug.Log("Equipment Type "+ typesStr[i] + " unreadable");
                            }
                        }

                        equipmentPrototypes.Add (equipmentPrototype);
					}
				} else {
					break;
				}
			}
		}

		private void getEquipmentPropertyDatabase(){
			StreamReader reader = new StreamReader ("Assets\\Scripts\\Equipment\\ItemPropertyDatabase.csv");
			reader.ReadLine ();

			string str = "";
			while(true){
				str = reader.ReadLine ();
				if (str!=null) {
					if(str.Trim().Length>0){
						string[] values = str.Split (";"[0]);
						EquipmentPropertyPrototype equipmentPropertyPrototype = new EquipmentPropertyPrototype ();

						equipmentPropertyPrototype.id = System.Int32.Parse (values[0]);
						equipmentPropertyPrototype.name = stringToPropertyName(values [1]);

						float.TryParse(values[2], out equipmentPropertyPrototype.valueOnTurnOff);
						float.TryParse(values[3], out equipmentPropertyPrototype.valueOnTurnOn);
						float.TryParse(values[4], out equipmentPropertyPrototype.valueOnActive);
						float.TryParse(values[5], out equipmentPropertyPrototype.valueOnOverdrive);
                        float.TryParse(values[6], out equipmentPropertyPrototype.valueOnOverheated);
                        float.TryParse(values[7], out equipmentPropertyPrototype.valueOnPoweringUp);
                        float.TryParse(values[8], out equipmentPropertyPrototype.valueOnPoweringDown);

                        equipmentPropertyPrototypes.Add (equipmentPropertyPrototype);
					}
				} else {
					break;
				}
			}
		}

		public Equipment getEquipment(int id){
			EquipmentPrototype eqPrototype = null;

			for (int i = 0; i < equipmentPrototypes.Count; i++) {
				if(id == equipmentPrototypes[i].id){
					eqPrototype = equipmentPrototypes [i];
					break;
				}
			}

			if (eqPrototype!=null) {
				Equipment eq = new Equipment();

				eq.id = eqPrototype.id;
				eq.iconName = eqPrototype.iconName;
				eq.name = eqPrototype.name;
				eq.description = eqPrototype.description;
				eq.mass = eqPrototype.mass;
                eq.grade = eqPrototype.grade;
                eq.state = State.TurnedOff; 
                eq.types = eqPrototype.types; 
                eq.requireTarget = eqPrototype.requireTarget;
				eq.canBeTurnOn = eqPrototype.canBeTurnOn;
				eq.canBeActivated = eqPrototype.canBeActivated;
				eq.canBeOverdrive = eqPrototype.canBeOverdrive;
                eq.poweringUpTime = eqPrototype.poweringUpTime;
                eq.poweringDownTime = eqPrototype.poweringDownTime;
                eq.durabilityMax = eqPrototype.durabilityMax;
				eq.heatMax = eqPrototype.heatMax;

				for (int i = 0; i < equipmentPropertyPrototypes.Count; i++) {
					if(eqPrototype.id == equipmentPropertyPrototypes[i].id){
						eq.addPropoerty (equipmentPropertyPrototypes[i].name, equipmentPropertyPrototypes[i].valueOnTurnOff, equipmentPropertyPrototypes[i].valueOnTurnOn, equipmentPropertyPrototypes[i].valueOnActive, equipmentPropertyPrototypes[i].valueOnOverdrive, equipmentPropertyPrototypes[i].valueOnOverheated, equipmentPropertyPrototypes[i].valueOnPoweringUp, equipmentPropertyPrototypes[i].valueOnPoweringDown);
					}
				}

				return eq;
			}

			Debug.Log ("NOT FOUND IN DB, "+equipmentPrototypes.Count);
			return null;
		}
	}


















































    public class EquipmentSum{
		Dictionary<PropertyName,float> propertiesSummed = new Dictionary<PropertyName,float>();
        private float mass;

        public EquipmentSum(Equipment[] eqArray)
        {
            mass = 0f;
            for (int i = 0; i < eqArray.Length; i++)
            {
                mass += eqArray[i].mass;
                foreach (PropertyName name in eqArray[i].properties.Keys)
                {
                    if (eqArray[i].hasProperty(name))
                    {
                        if (propertiesSummed.ContainsKey(name))
                        {
                            propertiesSummed[name] += eqArray[i].getPropertyValue(name);
                        }
                        else
                        {
                            propertiesSummed.Add(name, eqArray[i].getPropertyValue(name));
                        }
                    }
                }
            }
        }

        public float getMass() {
            return mass;
        }

        public float getPropertySum(PropertyName name){
			if (propertiesSummed.ContainsKey (name)) {
				return propertiesSummed [name];
			}
            if (Enum.IsDefined(typeof(PropertyName), name)==false)
            {
                Debug.Log("getPropertySum Error on name " + name);
            }
			return 0f;
		}

		public string report(){
			string str = "Mass = " + mass + "\n";
            foreach (PropertyName name in propertiesSummed.Keys) {
				str += name+" = "+propertiesSummed [name]+"\n";
			}
			//if (str.Length > 2) str = str.Trim ().Substring (0, str.Length - 2);
			return str;
		}
	}



















































	public enum PropertyName{
		None,
		MaxEnergy,
		EnergyProduction,
		EnergyConsumption,
		EnergyLocalBatteryCapacity,
		EnergyLocalBatteryChargeAmount,
		DurabilityLoss,
		HeatProduction,

		MaxShield,
		MaxArmor,
		MaxHull,
		ShieldRepair,
		ArmorRepair,
		HullRepair,

		DroneBandwitch,
		DroneAntenaRange,
		DroneSpeedBonus,
		DroneDamageBonus,
		DroneOperationsProfficiency,
		TargetingRange,
		NumberOfTargets,
        MissilesPerShot,

        Drag,
        DragAngular,
        EngineThrust,
        Maneuverability,

        CargoHold
    };

	public static PropertyName stringToPropertyName(string name){
		PropertyName property = PropertyName.None;
		try{
			property = (PropertyName) System.Enum.Parse (typeof(PropertyName), name);
		}catch{
			Debug.Log ("property unreadable");
		}

		return property;
	}




	public class EquipmentProperty{
		private PropertyName name = PropertyName.None;
		private float valueOnTurnOff = 0f;
		private float valueOnTurnOn = 0f;
		private float valueOnActive = 0f;
		private float valueOnOverdrive = 0f;
        private float valueOnOverheated = 0f;
        private float valueOnPoweringUp = 0f;
        private float valueOnPoweringDown = 0f;

        public EquipmentProperty(PropertyName _name, float _valueOnTurnOff, float _valueOnTurnOn, float _valueOnActive, float _valueOnOverdrive, float _valueOnOverheated, float _valueOnPoweringUp, float _valueOnPoweringDown){
			name = _name;
			valueOnTurnOff = _valueOnTurnOff;
			valueOnTurnOn = _valueOnTurnOn;
			valueOnActive = _valueOnActive;
			valueOnOverdrive = _valueOnOverdrive;
            valueOnOverheated = _valueOnOverheated;
            valueOnPoweringUp = _valueOnPoweringUp;
            valueOnPoweringDown = _valueOnPoweringDown;
        }

		public PropertyName getName(){ return name; }

		public float getAtState(Equipment.State state){
			switch(state){
				case State.TurnedOff: return valueOnTurnOff; break;
				case State.TurnedOn: return valueOnTurnOn; break;
				case State.Active: return valueOnActive; break;
				case State.Overdrive: return valueOnOverdrive; break;
                case State.Overheated: return valueOnOverheated; break;
                case State.PoweringUp: return valueOnPoweringUp; break;
                case State.PoweringDown: return valueOnPoweringDown; break;
                case State.Destroyed: return 0f; break;
				default: return 0f; break;
			}
		}
	}




























































    public enum Grade { Civil, Amateurish, Proffesional, Military, Technocratic };
    public enum State { TurnedOff, TurnedOn, Active, Overdrive, Overheated, Destroyed, PoweringUp, PoweringDown };
    public enum Type  {ShipCore, Energy, EnergyGenerator, EnergyBattery, Mobility, Engine, ManeuverabilityEnchancer, Offence, Weapon, MissileLauncher, Defence, Endurance, ShieldGenerator, ArmorPlate, HullReinforcement, RepairSystem, ShieldRepair, ArmorRepair, HullRepair, Targetting, TargetRange, TargetNumber};

    private int id = 0;
	private long uniqueId = 0;
	private string iconName = "";
	private string name = "";
	private string description = "";
	private float mass = 0f;
	private float size = 0f;
	private Equipment.Grade grade = Equipment.Grade.Amateurish;
	private Equipment.State state = Equipment.State.TurnedOff;
    private List<Equipment.Type> types = new List<Equipment.Type>();
    private bool requireTarget = false;
    private bool canBeTurnOn = false;
    private bool canBeActivated = false;
	private bool canBeOverdrive = false;

    private float poweringUpTime = 0f;
    private float poweringDownTime = 0f;
    private float poweringTimeActual = 0f;
    private float durability = 0f;
	private float durabilityMax = 0f;
	private float heat = 0f;
	private float heatMax = 0f;
	private float energyLocalBatteryCapacityCurrent = 0f;

	private ObjectInSpace target = null;
	private ObjectInSpace owner = null;
	private ShipPart shipPart = null;

	private Dictionary<PropertyName,EquipmentProperty> properties = new Dictionary<PropertyName,EquipmentProperty>();

	public int getId(){ return id; }
	public long getUniqueId(){ return uniqueId; }
	public string getIconName(){ return iconName; }
	public string getName(){ return name; }
	public string getDescription(){ return description; }
	public float getMass(){ return mass; }
	public float getSize(){ return size; }
	public Equipment.Grade getGrade(){ return grade; }
    public Equipment.State getState() { return state; }
    public List<Equipment.Type> getTypes() { return new List<Equipment.Type>(types.ToArray()); }
    public bool getRequireTarget(){ return requireTarget; }
	public bool getCanBeTurnOn(){ return canBeTurnOn; }
	public bool getCanBeActivated(){ return canBeActivated; }
	public bool getCanBeOverdrive(){ return canBeOverdrive; }

    public float getPoweringUpTime() { return poweringUpTime; }
    public float getPoweringDownTime() { return poweringDownTime; }
    public float getDurability(){ return durability; }
	public float getDurabilityMax(){ return durabilityMax; }
	public float getHeat(){ return heat; }
	public float getHeatMax(){ return heatMax; }
	public float getLocalBatteryCurrentCapacity(){ return energyLocalBatteryCapacityCurrent; }

	public ObjectInSpace getTarget(){ return target; }
    public ObjectInSpace getOwner() { return owner; }
    public ShipPart getShipPart() { return shipPart; }

    protected void setId(int _id){ id = _id; }
	protected void setUniqueId(long _uniqueId){ uniqueId = _uniqueId; }
	protected void setName(string _name){ name = _name; }
	protected void setIconName(string _iconName){ iconName = _iconName; }
	protected void setDescription(string _description){ description = _description; }
	protected void setMass(float _mass){ mass = _mass; }
	protected void setSize(float _size){ size = _size; }
	protected void setGrade(Equipment.Grade _grade){ grade = _grade; }
	protected void setRequireTarget(bool _requireTarget){ requireTarget = _requireTarget; }
	protected void setCanBeTurnOn(bool _canBeTurnOn){ canBeTurnOn = _canBeTurnOn; }
	protected void setCanBeActivated(bool _canBeActivated){ canBeActivated = _canBeActivated; }
	protected void setCanBeOverdrive(bool _canBeOverdrive){ canBeOverdrive = _canBeOverdrive; }

    protected void setPoweringUpTime(float _poweringUpTime) { poweringUpTime = _poweringUpTime; }
    protected void setPoweringDownTime(float _poweringDownTime) { poweringDownTime = _poweringDownTime; }
    protected void setDurability(float _durability){ durability = _durability; }
	protected void setDurabilityMax(float _durabilityMax){ durabilityMax = _durabilityMax; }
	protected void setHeat(float _heat){ heat = _heat; }
	protected void setHeatMax(float _heatMax){ heatMax = _heatMax; }

    public bool hasType(Type t) { return types.Contains(t); }
    public bool hasTarget() { if (target) { if (target.gameObject.active) { return true; } } return false; }

    public void setTarget(ObjectInSpace _target){ target = _target; /*if (shipPart) shipPart.setTarget (_target);*/ attemptActionOnActive(); }
	public void setOwner(ObjectInSpace _owner){ owner = _owner; /*if (shipPart) shipPart.setOwner (_owner); */ }
	public void setShipPart(ShipPart _shipPart){ shipPart = _shipPart; }

	private Equipment(){
		setUniqueId(GMS.instance().getUniqueId());
	}

    private Equipment(int _id, string _iconName, string _name, string _description, float _mass, Equipment.Grade _grade, Equipment.State _state, bool _requireTarget, bool _canBeTurnOn,  bool _canBeActivated, bool _canBeOverdrive, float _poweringUpTime, float _poweringDownTime, float _durabilityMax, float _heatMax) {
        setUniqueId(GMS.instance().getUniqueId());

        id = _id;
        iconName = _iconName;
        name = _name;
        description = _description;
        mass = _mass;
        grade = _grade;
        state = _state;
        requireTarget = _requireTarget;
        canBeTurnOn = _canBeTurnOn;
        canBeActivated = _canBeActivated;
        canBeOverdrive = _canBeOverdrive;
        poweringUpTime = _poweringUpTime;
        poweringDownTime = _poweringDownTime;
        durabilityMax = _durabilityMax;
        heatMax = _heatMax;
    }


	protected void addPropoerty(PropertyName name, float valueOnTurnOff, float valueOnTurnOn, float valueOnActive, float valueOnOverdrive, float valueOnOverheated, float valueOnPoweringUp, float valueOnPoweringDown){
		if(hasProperty(name)==false){
			properties.Add (name,new EquipmentProperty(name, valueOnTurnOff, valueOnTurnOn, valueOnActive, valueOnOverdrive, valueOnOverheated, valueOnPoweringUp, valueOnPoweringDown));
		}
	}

    protected void removePropoerty(PropertyName name){
		properties.Remove (name);
	}

	public float getPropertyValue(PropertyName name){
		if(hasProperty(name)) return properties [name].getAtState(state);
		return 0f;
	}

	public bool hasProperty(PropertyName name){
		return properties.ContainsKey (name);
	}

    public float getStateNumber()
    {
        switch (state)
        {
            case State.TurnedOff: return 1f; break;
            case State.TurnedOn: return 2f; break;
            case State.Active: return 3f; break;
            case State.Overdrive: return 4f; break;
            case State.Overheated: return 5f; break;
            case State.Destroyed: return 6f; break;
            case State.PoweringUp: return 7f; break;
            case State.PoweringDown: return 8f; break;
        }
        return 0f;
    }

    public string getStateName() {
        if (isInoperative()) return "Inoperative";
        return state+"";
    }

    public bool isInoperative() { return canBeTurnOn == false && canBeActivated == false && canBeOverdrive == false; }










































































    //States -----------------------------------------------------------------------------------------------------------------
    //public enum State { TurnedOff, TurnedOn, Active, Overdrive, Overheated, Destroyed, PoweringUp, PoweringDown };


    private State nextState = State.TurnedOff;

    public bool applyTime(float multiplier)
    {
        if (state == State.PoweringUp)
        {
            poweringTimeActual += multiplier;
            if (poweringUpTime == 0f)
            {
                state = nextState;
                onStateChange();
                return true;
            }
            if (poweringTimeActual >= poweringUpTime)
            {
                poweringTimeActual = poweringUpTime;
                state = nextState;
                onStateChange();
                return true;
            }
        }

        if (state == State.PoweringDown)
        {
            poweringTimeActual += multiplier;
            if (poweringDownTime == 0f)
            {
                turnOff();
                return true;
            }
            if (poweringTimeActual >= poweringDownTime)
            {
                poweringTimeActual = poweringDownTime;
                turnOff();
                return true;
            }
        }

        return false;
    }

    public float getPoweringPercent()
    {
        if (state == State.PoweringUp)
        {
            if (poweringUpTime >= 0f)
            {
                return poweringTimeActual / poweringUpTime;
            }
        }
        if (state == State.PoweringDown)
        {
            if (poweringDownTime >= 0f)
            {
                return poweringTimeActual / poweringDownTime;
            }
        }
        return -1f;
    }

    public bool turnOff()
    {
        if (state == State.TurnedOff || state == State.Overheated || state == State.Destroyed)
        {
            //do nothing
        }
        if (state == State.PoweringUp)
        {
            state = State.TurnedOff;
            onStateChange();
            energyLocalBatteryCapacityCurrent = 0f;
            poweringTimeActual = 0f;
            return true;
        }
        if (state == State.PoweringDown)
        {
            if(poweringTimeActual >= poweringDownTime)
            {
                state = State.TurnedOff;
                onStateChange();
                energyLocalBatteryCapacityCurrent = 0f;
                poweringTimeActual = 0f;
                return true;
            }
        }
        if (state == State.TurnedOn || state == State.Active || state == State.Overdrive)
        {
            if (poweringDownTime >= 0f)
            {
                state = State.PoweringDown;
                onStateChange();
                energyLocalBatteryCapacityCurrent = 0f;
                poweringTimeActual = 0f;
                return true;
            }
            else
            {
                state = State.TurnedOff;
                onStateChange();
                energyLocalBatteryCapacityCurrent = 0f;
                poweringTimeActual = 0f;
                return true;
            }
        }

        return false;
    }

    public bool turnOn(){
        if (canBeTurnOn)
        {
            if (state == State.TurnedOn || state == State.Overheated || state == State.Destroyed || state == State.PoweringUp || state == State.PoweringDown)
            {
                //do nothing
            }
            if (state == State.TurnedOff)
            {
                if (poweringUpTime >= 0f)
                {
                    state = State.PoweringUp;
                    nextState = State.TurnedOn;
                    onStateChange();
                    energyLocalBatteryCapacityCurrent = 0f;
                    poweringTimeActual = 0f;
                    return true;
                }
                else
                {
                    state = State.TurnedOn;
                    nextState = State.TurnedOn;
                    onStateChange();
                    energyLocalBatteryCapacityCurrent = 0f;
                    poweringTimeActual = 0f;
                    return true;
                }
            }
            if (state == State.Active || state == State.Overdrive)
            {
                state = State.TurnedOn;
                nextState = State.TurnedOn;
                onStateChange();
                return true;
            }
        }

        return false;
    }

    public bool activate() {
        if (canBeActivated)
        {
            if (state == State.Active || state == State.Overheated || state == State.Destroyed || state == State.PoweringUp || state == State.PoweringDown)
            {
                //do nothing
            }
            if (state == State.TurnedOff)
            {
                if (poweringUpTime >= 0f)
                {
                    state = State.PoweringUp;
                    nextState = State.Active;
                    onStateChange();
                    energyLocalBatteryCapacityCurrent = 0f;
                    poweringTimeActual = 0f;
                    return true;
                }
                else
                {
                    state = State.Active;
                    nextState = State.Active;
                    onStateChange();
                    energyLocalBatteryCapacityCurrent = 0f;
                    poweringTimeActual = 0f;
                    return true;
                }
            }
            if (state == State.TurnedOn || state == State.Overdrive)
            {
                state = State.Active;
                onStateChange();
                attemptActionOnActive();
                return true;
            }
        }

        return false;
    }

    public bool overdrive()
    {
        if (canBeOverdrive)
        {
            if (state == State.Overdrive || state == State.Overheated || state == State.Destroyed || state == State.PoweringUp || state == State.PoweringDown)
            {
                //do nothing
            }
            if (state == State.TurnedOff)
            {
                if (poweringUpTime >= 0f)
                {
                    state = State.PoweringUp;
                    nextState = State.Overdrive;
                    onStateChange();
                    energyLocalBatteryCapacityCurrent = 0f;
                    poweringTimeActual = 0f;
                    return true;
                }
                else
                {
                    state = State.Overdrive;
                    nextState = State.Overdrive;
                    onStateChange();
                    energyLocalBatteryCapacityCurrent = 0f;
                    poweringTimeActual = 0f;
                    return true;
                }
            }
            if (state == State.TurnedOn || state == State.Active)
            {
                state = State.Overdrive;
                onStateChange();
                attemptActionOnActive();
                return true;
            }
        }

        return false;
    }

    public bool setState(State newState)
    {
        bool retVal = false;
        if (newState == State.TurnedOff) retVal = turnOff();
        if ( newState == State.TurnedOn) retVal = turnOn();
        if (   newState == State.Active) retVal = activate();
        if (newState == State.Overdrive) retVal = overdrive();

        if(retVal) owner.setNeedRecalculateEquipment();

        return retVal;
    }

    protected void onStateChange() {
        if (shipPart) {
            shipPart.onStateChange();
        }
    }

    private void changeStateInternal(State newState, State nxtState)
    {
        state = newState;
        nextState = nxtState;
        onStateChange();
    }




    private State queuedState = State.TurnedOff;
    private bool queuedStateAttemptChange = false;

    public State getQueuedState()
    {
        return queuedState;
    }

    public bool queueState(State newQueuedState)
    {
        if (newQueuedState == State.TurnedOff || newQueuedState == State.TurnedOn || newQueuedState == State.Active || newQueuedState == State.Overdrive)
        {
            queuedState = newQueuedState;
            queuedStateAttemptChange = true;
        }
        return false;
    }

    public bool attemptToChangeQueuedState()
    {
        //public enum State { TurnedOff, TurnedOn, Active, Overdrive, Overheated, Destroyed, PoweringUp, PoweringDown };
        if (queuedStateAttemptChange)
        {
            if (state == State.TurnedOff || state == State.TurnedOn || state == State.Active || state == State.Overdrive)
            {
                if (state != queuedState)
                {
                    bool stateChangeSuccess = setState(queuedState);
                    if (stateChangeSuccess)
                    {
                        queuedStateAttemptChange = false;
                    }
                    
                }
            }
        }

        return false;
    }















































    //Energy, Heat, Durability -----------------------------------------------------------------------------------------------------------------




    public float energyProduction(){
		return getPropertyValue (PropertyName.EnergyProduction);
	}

	public float energyRequirementPassive(){
		return getPropertyValue (PropertyName.EnergyConsumption);
	}

	public float energyRequirementActive(){
		float energyLocalBatteryCapacity = getPropertyValue (PropertyName.EnergyLocalBatteryCapacity);
		if (energyLocalBatteryCapacity > 0f) {
			if (energyLocalBatteryCapacityCurrent >= energyLocalBatteryCapacity) return 0f;
			return getPropertyValue (PropertyName.EnergyLocalBatteryChargeAmount);
		}
		return 0f;
	}

	public bool hasEnergyForPassive(float amount){
		//if amount is enough, nothing happens, energy just dissapear. If it is not enough, the equipment is turned off
		return (amount >= getPropertyValue (PropertyName.EnergyConsumption));
	}

	public float addEnergyLocalToBattery(float amount) {
		if (amount > 0f) {
			float energyLocalBatteryCapacity = getPropertyValue (PropertyName.EnergyLocalBatteryCapacity);
			if (energyLocalBatteryCapacity > 0f) {
				if (state == State.Active || state == State.Overdrive) {
					energyLocalBatteryCapacityCurrent += amount;
					amount = 0f;
                    attemptActionOnActive();
                } else{
					energyLocalBatteryCapacityCurrent += amount;
					if (energyLocalBatteryCapacityCurrent >= energyLocalBatteryCapacity) {
						amount = energyLocalBatteryCapacityCurrent - energyLocalBatteryCapacity;
						energyLocalBatteryCapacityCurrent = energyLocalBatteryCapacity;
					} else {
						amount = 0f;
					}
				}
			}
		}
		return amount;
	}

    protected void attemptActionOnActive()
    {
        if (state == State.Active || state == State.Overdrive)
        {
            if (requireTarget)
            {
                if (target)
                {
                    if (energyLocalBatteryCapacityCurrent >= getPropertyValue(PropertyName.EnergyLocalBatteryCapacity))
                    {
                        energyLocalBatteryCapacityCurrent -= getPropertyValue(PropertyName.EnergyLocalBatteryCapacity);
                        actionOnActive();
                    }
                }
            }
            else
            {
                if (energyLocalBatteryCapacityCurrent >= getPropertyValue(PropertyName.EnergyLocalBatteryCapacity))
                {
                    energyLocalBatteryCapacityCurrent -= getPropertyValue(PropertyName.EnergyLocalBatteryCapacity);
                    actionOnActive();
                }
            }
        }
    }

    protected void actionOnActive(){
		if (shipPart) {
            shipPart.actionOnActive();
        }
	}

	public bool applyHeat(float multiplier){
		if (state != State.Destroyed && heatMax > 0f) {
			heat += getPropertyValue (PropertyName.HeatProduction) * multiplier;
			if (heat < 0f) {
				heat = 0f;
				if (state == State.Overheated) {

                    if (poweringDownTime >= 0f)
                    {
                        state = State.PoweringDown;
                        onStateChange();
                        energyLocalBatteryCapacityCurrent = 0f;
                        poweringTimeActual = 0f;
                    }
                    else
                    {
                        state = State.TurnedOff;
                        onStateChange();
                        energyLocalBatteryCapacityCurrent = 0f;
                        poweringTimeActual = 0f;
                    }

					return true;
				}
			}
			if (heat >= heatMax) {
				state = State.Overheated;
                onStateChange();
                energyLocalBatteryCapacityCurrent = 0f;
                applyDurabilityAmount(durabilityMax * 0.1f, 1f);
                return true;
			}
		}
		return false;
	}

    private bool applyDurabilityAmount(float amount, float multiplier){
        if (state != State.Destroyed && durabilityMax > 0f){
            durability += amount * multiplier;
            if (durability >= durabilityMax){
                state = State.Destroyed;
                onStateChange();
                energyLocalBatteryCapacityCurrent = 0f;
                heat = 0f;
                durability = durabilityMax;
                return true;
            }
        }
        return false;
    }

    public bool applyDurability(float multiplier){
        return applyDurabilityAmount(getPropertyValue(PropertyName.DurabilityLoss), multiplier);
    }

    /*
    public bool applyDurability(float multiplier)
    {
        if (state != State.Destroyed && durabilityMax > 0f)
        {
            durability += getPropertyValue(PropertyName.DurabilityLoss) * multiplier;
            if (durability >= durabilityMax)
            {
                state = State.Destroyed;
                onStateChange();
                energyLocalBatteryCapacityCurrent = 0f;
                heat = 0f;
                durability = durabilityMax;
                return true;
            }
        }
        return false;
    }
    */


    public static bool isEquipmentWithTypeOperateable(Equipment[] eq, Equipment.Type type) {
        for (int i = 0; i < eq.Length; i++) {
            if (new List<Type>(eq[i].getTypes()).Contains(type)) {
                if (eq[i].getState() == State.TurnedOn || eq[i].getState() == State.Active || eq[i].getState() == State.Overdrive) {
                    return true;
                }
            }
        }
        return false;
    }



















	public string json(){
		string str = "";
        str += "id:" + id + ", ";
        str += "name:" + name + ", ";
        str += "description:" + description + ", ";
        str += "mass:" + mass + ", ";
        str += "grade:" + grade + ", ";
        str += "state:" + state + ", ";
        str += "types: [";
        for(int i=0;i<types.Count;i++) if(i< types.Count - 1) str += types[i] + ", "; else str += types[i];
        str += "], ";
        str += "target:" + target + ", ";
        str += "owner:" + owner + ", ";
        str += "requireTarget:" + requireTarget + ", ";
        str += "canBeTurnOn:" + canBeTurnOn + ", ";
        str += "canBeActivated:" + canBeActivated + ", ";
        str += "canBeOverdrive:" + canBeOverdrive + ", ";

        str += "poweringUpTime:" + poweringUpTime + ", ";
        str += "poweringTimeActual:" + poweringTimeActual + ", ";
        str += "poweringDownTime:" + poweringDownTime + ", ";
        str += "poweringTimeActual:" + poweringTimeActual + ", ";
        str += "durability:" + durability + ", ";
        str += "durabilityMax:" + durabilityMax + ", ";
        str += "heat:" + heat + ", ";
        str += "heatMax:" + heatMax + ", ";
        return str.Substring(0,str.Length-1);
	}


	public string report(){
		string str = "";

		str += name + "(" + uniqueId + ") ";
		str += "S: " + getState () + ", ";
		str += "Pr: " + energyProduction () + ", ";
		str += "Pa: " + energyRequirementPassive () + ", ";
		str += "A: " + energyRequirementActive () + ", ";
		str += "B: " + energyLocalBatteryCapacityCurrent.ToString ("F2") + "/" + getPropertyValue (PropertyName.EnergyLocalBatteryCapacity) + ", ";
		str += "H: " + heat.ToString ("F2") + "/" + heatMax + ", ";
		str += "D: " + durability.ToString ("F2") + "/" + durabilityMax;

		return str;
	}








}





