namespace Personagens
{
    abstract class Personagem
    {
        protected string Nome { get; set; }
        protected int Vida { get; set; }
        protected int Mana { get; set; }
        protected double XP { get; set; }
        protected int Inteligencia { get; set; }
        protected int Forca { get; set; }
        protected int Level { get; set; }
        public static int TotalDePersonagens = 0;
        public Personagem() { }
        public Personagem(string nome, int vida, int mana, double xp, int inteligencia, int forca, int level)
        {
            Nome = nome;
            Vida = vida;
            Mana = mana;
            XP = xp;
            Inteligencia = inteligencia;
            Forca = forca;
            Level = level;
            TotalDePersonagens++;
        }

        public abstract void LvlUp();
        public abstract void Attack();

        
    }
}