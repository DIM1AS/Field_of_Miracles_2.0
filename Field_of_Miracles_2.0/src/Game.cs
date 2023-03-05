using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Field_of_Miracles_2._0.src
{
    class Game
    {
        private int _score = 0;
        private int _barabanScore = 0;
        private int _stage = 1;
        private bool _barabanState = false;

        private KeyValuePair<string, string> _currentQuestion;
        private List<string> _alreadyUsedQuestions = new List<string>();

        public Random Random = new Random();

        private Dictionary<string, string> _firstQuestions = new Dictionary<string, string>()
        {
            {"Что не купишь ни за какие деньги?", "Здоровье" }, // первый этап
            {"Как называется массовое заболевание людей?", "Эпидемия" },
            {"Рациональное распределение времени в течение суток?", "Режим" },
        };

        private Dictionary<string, string> _secondQuestions = new Dictionary<string, string>()
        {
            {"Тренировка организма холодом?", "Закаливание" }, // второй этап
            {"Как называется наука о чистоте?", "Гигиена" },
            {"Мельчайший организм, переносящий инфекцию?", "Микроб" },
        };

        private Dictionary<string, string> _thirdQuestions = new Dictionary<string, string>()
        {
            {"Она является залогом здоровья?", "Чистота" },
            {"Незаразная болезнь?", "Кариес" },
        };

        public int GetScore => _score;

        public void SetScore(int score) => _score = score;

        public void SetStage(int stage)
        {
            _stage = stage;
        }

        public int GetStage => _stage;

        public void ChangeBarabanState() => _barabanState = !_barabanState;

        public bool GetBarabanState => _barabanState;

        public void SetBarabanScore(int score) => _barabanScore = score;

        public int GetBarabanScore => _barabanScore;

        public KeyValuePair<string, string> GetQuestion()
        {
            KeyValuePair<string, string> question;

            if (_stage == 1)
            {
                do
                {
                    question = _firstQuestions.ElementAt(Random.Next(_firstQuestions.Count));
                } while (_alreadyUsedQuestions.Contains(question.Key));

                return question;
            }
            else if (_stage == 2)
            {
                do
                {
                    question = _secondQuestions.ElementAt(Random.Next(_secondQuestions.Count));
                } while (_alreadyUsedQuestions.Contains(question.Key));

                return question;
            }
            else if (_stage == 3)
            {
                do
                {
                    question = _thirdQuestions.ElementAt(Random.Next(_thirdQuestions.Count));
                } while (_alreadyUsedQuestions.Contains(question.Key));

                return question;
            }
            else
            {
                return new KeyValuePair<string, string>();
            }
        }

        public void SetCurrentQuestion(KeyValuePair<string, string> question)
        {
            _currentQuestion = question;
        }

        public KeyValuePair<string, string> GetCurrentQuestion => _currentQuestion;

        public string WordToSymbols(string word) => new string('*', word.Length);

        public string CreateEmptyWord(string word) => new string('*', word.Length);

        public void AddToAlreadyUsedQuestions(string question)
        {
            _alreadyUsedQuestions.Add(question);
        }

        public List<string> GetAlreadyUsedQuestions => _alreadyUsedQuestions;

        public Dictionary<string, string> GetQuestionsByStage()
        {
            if (_stage == 1) return _firstQuestions;
            else if (_stage == 2) return _secondQuestions;
            else return _thirdQuestions;
        }
    }
}
