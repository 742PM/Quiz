namespace Infrastructure.Result
{
    public sealed class None
    {
        public static None Nothing => new None();

        private None() { }
    }
}