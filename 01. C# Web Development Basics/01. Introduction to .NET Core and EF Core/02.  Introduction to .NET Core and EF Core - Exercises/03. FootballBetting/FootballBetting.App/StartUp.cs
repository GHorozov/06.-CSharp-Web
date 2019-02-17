namespace FootballBetting.App
{
    using FootballBetting.Data;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new FootballBettingDbContext();

            InitiateDatabase(context);
        }

        private static void InitiateDatabase(FootballBettingDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
