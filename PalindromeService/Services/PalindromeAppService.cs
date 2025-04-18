using PalindromeServices.Services;

namespace PalindromeServices.Services
{
    public class PalindromeAppService : IPalindromeService
    {
        public async Task<bool> IsPalindrome(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            int left = 0;
            int right = input.Length - 1;
            while (left < right)
            {
                if (input[left] != input[right])
                {
                    return false;
                }

                left++;
                right--;
            }

            return await Task.Run(() => true);
        }
    }
}
