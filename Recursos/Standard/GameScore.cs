using System;
using System.Collections.Generic;
using System.Text;

namespace AppScoreExemplo.Model
{
    public class GameScore
    {
        public int id { get; set; }
        public int highscore { get; set; }
        public string game { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phrase { get; set; }
    }
}
