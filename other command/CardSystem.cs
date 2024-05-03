using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_App.other_command
{
    public class CardSystem
    {
        private string[] cardNum = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "Ace" };
        private string[] cardSuits = { "Bich", "Nhep", "Ro", "Co" };

        public string SelectedNumber { get; set; }
        public string SelectedCard { get; set; }
        public int CardPowerNum { get; set; }
        public int CardPowerSuits { get; set; }
        
        public CardSystem() 
        {
            var random = new Random();
            int numberIndex = random.Next(0, cardNum.Length - 1);
            int suitIndex = random.Next(0, cardSuits.Length - 1);

            this.SelectedNumber = cardNum[numberIndex];
            this.SelectedCard = $"{cardNum[numberIndex]} {cardSuits[suitIndex]}";
            this.CardPowerNum = numberIndex;
            this.CardPowerSuits = suitIndex;

        }
    }
}
