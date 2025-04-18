namespace PalindromeServices.Services
{
    public interface IPalindromeService
    {
        Task<bool> IsPalindrome(string input);
    }
}
