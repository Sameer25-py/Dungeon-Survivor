namespace DungeonSurvivor.Core.ID
{
    public static class IDManager
    {
        private static int s_gateID = 0;
        private static int s_enemyID = 1;

        public static int AssignGateID()
        {
            s_gateID++;
            return s_gateID;
        }

        public static int AssignEnemyID()
        {
            s_enemyID++;
            return s_enemyID;
        }

    }
}
