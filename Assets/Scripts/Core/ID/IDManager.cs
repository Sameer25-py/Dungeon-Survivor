namespace DungeonSurvivor.Core.ID
{
    public static class IDManager
    {
        private static int s_gateID = 0;

        public static int AssignGateID()
        {
            s_gateID++;
            return s_gateID;
        }

    }
}
