using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalEquipmentGenerator{


    private class EquipmentPrototype
    {
        public int id = 0;
        public string iconName = "";
        public string name = "";
        public string description = "";
        public float mass = 0f;
        public float size = 0f;
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




    /*

    public class EquipmentDatabase
    {

        private class EquipmentPrototype
        {
            public int id = 0;
            public string iconName = "";
            public string name = "";
            public string description = "";
            public float mass = 0f;
            public float size = 0f;
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
            public string print() { return id + ", " + name + ", " + valueOnTurnOff + ", " + valueOnTurnOn + ", " + valueOnActive + ", " + valueOnOverdrive + ", " + valueOnOverheated + ", " + valueOnPoweringUp + ", " + valueOnPoweringDown; }
        }

        private List<EquipmentPrototype> equipmentPrototypes;
        private List<EquipmentPropertyPrototype> equipmentPropertyPrototypes;

        public EquipmentDatabase()
        {
            equipmentPrototypes = new List<EquipmentPrototype>();
            equipmentPropertyPrototypes = new List<EquipmentPropertyPrototype>();
            getEquipmentDatabase();
            getEquipmentPropertyDatabase();
        }

        private void getEquipmentDatabase()
        {
            StreamReader reader = new StreamReader("Assets\\Scripts\\Equipment\\ItemDatabase.csv");
            reader.ReadLine();

            string str = "";
            while (true)
            {
                str = reader.ReadLine();
                if (str != null)
                {
                    if (str.Trim().Length > 0)
                    {
                        string[] typesStr;
                        string[] values = str.Split(";"[0]);
                        EquipmentPrototype equipmentPrototype = new EquipmentPrototype();
                        int index = 0;

                        equipmentPrototype.id = System.Int32.Parse(values[index]); index++;
                        equipmentPrototype.iconName = values[index]; index++;
                        equipmentPrototype.name = values[index]; index++;
                        equipmentPrototype.description = values[index]; index++;
                        typesStr = values[index].Split(','); ; index++;
                        float.TryParse(values[index], out equipmentPrototype.mass); index++;
                        float.TryParse(values[index], out equipmentPrototype.size); index++;

                        if (values[index].Equals("Amateurish")) equipmentPrototype.grade = Equipment.Grade.Amateurish;
                        if (values[index].Equals("Civil")) equipmentPrototype.grade = Equipment.Grade.Civil;
                        if (values[index].Equals("Proffesional")) equipmentPrototype.grade = Equipment.Grade.Proffesional;
                        if (values[index].Equals("Military")) equipmentPrototype.grade = Equipment.Grade.Military;
                        if (values[index].Equals("Technocratic")) equipmentPrototype.grade = Equipment.Grade.Technocratic;
                        index++;

                        if (values[index].ToLower().Equals("true")) equipmentPrototype.requireTarget = true;
                        if (values[index].ToLower().Equals("false")) equipmentPrototype.requireTarget = false;
                        index++;

                        if (values[index].ToLower().Equals("true")) equipmentPrototype.canBeTurnOn = true;
                        if (values[index].ToLower().Equals("false")) equipmentPrototype.canBeTurnOn = false;
                        index++;

                        if (values[index].ToLower().Equals("true")) equipmentPrototype.canBeActivated = true;
                        if (values[index].ToLower().Equals("false")) equipmentPrototype.canBeActivated = false;
                        index++;

                        if (values[index].ToLower().Equals("true")) equipmentPrototype.canBeOverdrive = true;
                        if (values[index].ToLower().Equals("false")) equipmentPrototype.canBeOverdrive = false;
                        index++;

                        float.TryParse(values[index], out equipmentPrototype.poweringUpTime); index++;
                        float.TryParse(values[index], out equipmentPrototype.poweringDownTime); index++;
                        float.TryParse(values[index], out equipmentPrototype.durabilityMax); index++;
                        float.TryParse(values[index], out equipmentPrototype.heatMax); index++;

                        Equipment.Type eqType;
                        for (int i = 0; i < typesStr.Length; i++)
                        {
                            try
                            {
                                eqType = (Equipment.Type)System.Enum.Parse(typeof(Equipment.Type), typesStr[i].Trim());
                                equipmentPrototype.types.Add(eqType);
                            }
                            catch
                            {
                                Debug.Log("Equipment Type " + typesStr[i] + " unreadable");
                            }
                        }

                        equipmentPrototypes.Add(equipmentPrototype);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void getEquipmentPropertyDatabase()
        {
            StreamReader reader = new StreamReader("Assets\\Scripts\\Equipment\\ItemPropertyDatabase.csv");
            reader.ReadLine();

            string str = "";
            while (true)
            {
                str = reader.ReadLine();
                if (str != null)
                {
                    if (str.Trim().Length > 0)
                    {
                        string[] values = str.Split(";"[0]);
                        EquipmentPropertyPrototype equipmentPropertyPrototype = new EquipmentPropertyPrototype();

                        equipmentPropertyPrototype.id = System.Int32.Parse(values[0]);
                        equipmentPropertyPrototype.name = stringToPropertyName(values[1]);

                        float.TryParse(values[2], out equipmentPropertyPrototype.valueOnTurnOff);
                        float.TryParse(values[3], out equipmentPropertyPrototype.valueOnTurnOn);
                        float.TryParse(values[4], out equipmentPropertyPrototype.valueOnActive);
                        float.TryParse(values[5], out equipmentPropertyPrototype.valueOnOverdrive);
                        float.TryParse(values[6], out equipmentPropertyPrototype.valueOnOverheated);
                        float.TryParse(values[7], out equipmentPropertyPrototype.valueOnPoweringUp);
                        float.TryParse(values[8], out equipmentPropertyPrototype.valueOnPoweringDown);

                        equipmentPropertyPrototypes.Add(equipmentPropertyPrototype);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public Equipment getEquipment(int id)
        {
            EquipmentPrototype eqPrototype = null;

            for (int i = 0; i < equipmentPrototypes.Count; i++)
            {
                if (id == equipmentPrototypes[i].id)
                {
                    eqPrototype = equipmentPrototypes[i];
                    break;
                }
            }

            if (eqPrototype != null)
            {
                Equipment eq = new Equipment();

                eq.id = eqPrototype.id;
                eq.iconName = eqPrototype.iconName;
                eq.name = eqPrototype.name;
                eq.description = eqPrototype.description;
                eq.mass = eqPrototype.mass;
                eq.size = eqPrototype.size;
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

                for (int i = 0; i < equipmentPropertyPrototypes.Count; i++)
                {
                    if (eqPrototype.id == equipmentPropertyPrototypes[i].id)
                    {
                        eq.addPropoerty(equipmentPropertyPrototypes[i].name, equipmentPropertyPrototypes[i].valueOnTurnOff, equipmentPropertyPrototypes[i].valueOnTurnOn, equipmentPropertyPrototypes[i].valueOnActive, equipmentPropertyPrototypes[i].valueOnOverdrive, equipmentPropertyPrototypes[i].valueOnOverheated, equipmentPropertyPrototypes[i].valueOnPoweringUp, equipmentPropertyPrototypes[i].valueOnPoweringDown);
                    }
                }

                return eq;
            }

            Debug.Log("NOT FOUND IN DB, " + equipmentPrototypes.Count);
            return null;
        }
    }





    */




}
