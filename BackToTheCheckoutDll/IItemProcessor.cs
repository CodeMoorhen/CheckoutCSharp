namespace BackToTheCheckoutDll
{
    public interface IItemProcessor
    {
        void Scan(string Item);
        int Total();
    }
}