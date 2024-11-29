using System;

namespace LabaProga3
{
    public class GameLogger
    {

    
    private static GameLogger instance;

        
        private GameLogger() { }

        
        public static GameLogger GetInstance()
        {
            if (instance == null)
            {
                instance = new GameLogger();
            }

            return instance;
        }

        
        public void Log(String message)
        {
            Console.WriteLine("[GAME LOG]: " + message);
        }
    }
}
