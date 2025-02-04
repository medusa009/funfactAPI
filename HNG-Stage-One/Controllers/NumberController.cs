using Microsoft.AspNetCore.Mvc;

namespace HNG_Stage_One.Controllers;

[Route("api/classify-number")]
[ApiController]
public class NumberController : ControllerBase
{
    private readonly HttpClient _httpclient;

    public NumberController(HttpClient httpclient)
    {
        _httpclient = httpclient;
    }
    // GET
    [HttpGet]
    public async Task<IActionResult> GetNumberClassification([FromQuery] string number)
    {
        if (!int.TryParse(number, out int num))
        {
            return BadRequest(new { number, error = true});
        }
        
        // Fetch Fun Fact 
        string funFact = await GetFunFactAsync(num);
        
        // Determine Properties
        bool isPrime = IsPrime(num);
        bool isPerfect = IsPerfect(num);
        bool isArmstrong = IsArmstrong(num);
        bool isOdd = num % 2 != 0;

        var properties = new List<String>();
        if(isArmstrong) properties.Add("armstrong");
        properties.Add(isOdd ? "odd" : "even");

        return Ok(new
        {
            number = num,
            is_prime = isPrime,
            is_perfect = isPerfect,
            properties,
            digit_sum = GetDigitSum(num),
            fun_fact = funFact,

        });
        
    }

    private async Task<string> GetFunFactAsync(int num)
    {
        try
        {
            string url = $"http://numbersapi.com/{num}/math";
            return await _httpclient.GetStringAsync(url);
        }
        catch
        {
            return "Could not retrieve a fun fact.";
        }
    }

    private bool IsPrime(int num)
    {
        if (num < 2) return false;
        for(int i = 2; i * i <= num; i++)
            if(num % i == 0) return false;
        return true;
    }

    private bool IsPerfect(int num)
    {
        int sum = 1;
        for (int i = 2; i * i <= num; i++)
        {
            if (num % i == 0)
            {
                sum += i;
                if (i != num / i) sum += num / i;
            }
        }

        return sum == num && num != 1;
    }

    private bool IsArmstrong(int num)
    {
        int sum = 0, temp = num, digits = num.ToString().Length;
        while (temp > 0)
        {
            int digit = temp % 10;
            sum += (int)Math.Pow(digit, digits);
            temp /= 10;
        }

        return sum == num;
    }

    private int GetDigitSum(int num)
    {
        int sum = 0;
        while (num > 0)
        {
            sum += num % 10;
            num /= 10;
        }

        return sum;
    }
        
    
}