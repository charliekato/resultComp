namespace ResultComp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new SelectServer());
        }
        public static string Right(string input, int length)
        {
            string head = new string(' ', length);

            string untrim = head + input;
            return untrim.Substring(untrim.Length-length, length);

        }
        public static string Space(int length)
        {
            if (length <= 0)
            {
                return string.Empty;
            }
            return new string(' ', length);
        }
    }
}