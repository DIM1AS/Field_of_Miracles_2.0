using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Field_of_Miracles_2._0.src;
using System.Diagnostics;

namespace Field_of_Miracles_2._0
{
    public partial class Form2 : Form
    {
        Game game = new Game();
        Thread th;
        public Form2()
        {
            InitializeComponent();

            InitNewGame(1);
        }

        public void label1_Click(object sender, EventArgs e)
        {
            if (game.GetBarabanState)
            {
                var labelSender = (Label)sender;
                var clickedLetter = labelSender.Text.ToLower();

                var currentWord = game.GetCurrentQuestion.Value.ToLower();

                if (currentWord.Contains(clickedLetter)) // если слово содержит букву 
                {
                    GuessWord(wordLabel.Text, currentWord, clickedLetter, wordLabel);

                    UpdateLabelText(scoreLabel, game.GetScore.ToString());
                    UpdateLabelText(randomScoreLabel, game.GetBarabanScore.ToString());

                    if (currentWord == wordLabel.Text) // если слово угадано полностью
                    {
                        MessageBox.Show("Слово угадано!");
                        game.AddToAlreadyUsedQuestions(game.GetCurrentQuestion.Key);

                        if (game.GetQuestionsByStage().Keys.All(k => game.GetAlreadyUsedQuestions.Contains(k)))
                        {
                            MessageBox.Show($"Все слова {GetStageByInt(game.GetStage)} этапа угаданы!");

                            InitNewGame(game.GetStage + 1);
                        }
                        else
                        {
                            InitNewGame(null);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Данной буквы нет в слове!");

                    UpdateLabelText(randomScoreLabel, "0");

                    game.SetBarabanScore(0);
                    game.ChangeBarabanState();
                }
            }
            else
            {
                MessageBox.Show("Вы не крутили барабан!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            game.SetBarabanScore(game.Random.Next(300));
            randomScoreLabel.Text = game.GetBarabanScore.ToString();

            game.ChangeBarabanState();
        }

        private void UpdateLabelText(Label label, string text) => label.Text = text;

        private void GuessWord(string wordWithStars, string originalWord, string letter, Label wordLabel)
        {
            if (!game.GetBarabanState) MessageBox.Show("Вы не крутили барабан!"); 

            if (wordWithStars.Contains(letter))
            {
                game.SetBarabanScore(0);
                game.ChangeBarabanState();
                MessageBox.Show("Вы уже открыли эту букву ранее!");
                return;
            }

            for (int i = 0; i < originalWord.Length; i++)
            {
                if (originalWord[i] == Convert.ToChar(letter))
                {
                    wordWithStars = wordWithStars.Remove(i, 1).Insert(i, Convert.ToChar(letter).ToString());
                }
            }

            game.SetScore(game.GetScore + game.GetBarabanScore);
            game.SetBarabanScore(0);
            game.ChangeBarabanState();

            wordLabel.Text = wordWithStars;

            Debug.WriteLine(game.GetBarabanState);
        }

        public void InitNewGame(int? stage)
        {          
            if (stage.HasValue) game.SetStage(stage.Value);
            if (stage.HasValue && stage > 3)
            {
                MessageBox.Show("Все этапы завершены! Поздравляем!");
                this.Close();
                th = new Thread(open_1);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                return;
            }

            var randomQuestion = game.GetQuestion();

            game.SetCurrentQuestion(randomQuestion);

            UpdateLabelText(wordLabel, game.CreateEmptyWord(randomQuestion.Value));
            UpdateLabelText(questionLabel, randomQuestion.Key);

            label39.Text = game.GetStage.ToString();
        }

        private void open_1(object obg)
        {
            Application.Run(new Form1());
        }

        private string GetStageByInt(int stage)
        {
            if (stage == 1) { return "Первого"; }
            else if (stage == 2) { return "Второго"; }
            else if (stage == 3) { return "Третьего"; }
            else return null;
        }
    }
}
