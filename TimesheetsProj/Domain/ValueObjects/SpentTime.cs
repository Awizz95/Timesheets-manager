namespace TimesheetsProj.Domain.ValueObjects
{
    public class SpentTime : ValueObject
    {
        private SpentTime() { }

        private SpentTime(int amount)
        {
            Amount = amount;
        }

        public int Amount { get; }

        public static SpentTime FromInt(int amount)
        {
            if (amount < 0 || amount > 8)
            {
                throw new ArgumentException("Amount cannot should be between 0 and 8.");
            }

            return new SpentTime(amount);
        }
    }
}
