namespace EntityWorld.Interfaces
{
    public interface IRandomNumberGenerator
    {
        /// <summary>
        /// Generate a number
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        int Next(int minimum, int maximum);
    }
}