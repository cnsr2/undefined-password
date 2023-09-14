using undefined_password.Models;

namespace undefined_password.Business
{
    public class RandomService
    {
        RandomPw randomPw = new RandomPw
        {
            _lowerCase = "abcdefghijklmnopqrstuvwxyz",
            _upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            _numbers = "0123456789",
            _symbols = "!@#$%^&*()+~`|}{[]:;?><,./-="
        };
        public string CreateRandomPw(bool _low,bool _upp,bool _num,bool _sym,int _length)
        {
            string tempPw = "";
            string _pw = "";

            if (_low)
            {
                tempPw += randomPw._lowerCase;
            }
            if (_upp)
            {
                tempPw += randomPw._upperCase;
            }
            if (_num)
            {
                tempPw += randomPw._numbers;
            }
            if (_sym)
            {
                tempPw += randomPw._symbols;
            }
            Random rastgele = new Random();
            for (int i = 0; i < _length; i++)
            {
                _pw += tempPw[rastgele.Next(tempPw.Length)];
            }
            return _pw;
        }
    }
}
