using System;
using System.Threading;
using static System.Console;  

namespace Guerra_Galattica
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Battaglia_galattica";
            bool vero=false;
            int sparo = 10; // Velocità dello sparo della navicella nemica.
            int fermo = 25; //velocita del gioco.
            int xa = 60; //coordinata x dell anostronave nemica.
            int ysn = 4; // Coordinata y dello sparo della navicella.
            int xsn = xa + 10; // Coordinata x dello sparo della navicella.
            int vite = 3; // Numero di vite.
            bool movimento = false;// Diventa true solo se l'astronave bassa sale o scende e serve solo per cancellare la vecchia. 
            bool primoSparo = false;
            bool ok;

            // Altezza finesta
            WindowHeight = 35;

            //Schermata iniziale con il regolamento;
            Console.Write("Regolamento:" +
                "\nSe colpisci la navicella nemica sali di livello, e il gioco si velocizza sempre di più!" +
                "\nCi sono un totale di 8 livelli! Ma non credo che supererai il 5..." +
                "\nSolo nel primo livello hai tre vite per superarlo se no GAME OVER!!!" +
                "\nDal secondo livello in su se vieni colpito ritorni al livello precedente." +
                "\nI comandi sono: Barra spaziatrice per sparare, frecce direzionali destra e sinistra per spostarsi, freccia in alto per salire." +
                "\nSe il programma ti ha fatto arrabbiare basta premere esc e lui scomparirà..." +
                "\nSe ti gusta saltare da un livello a un altro basta premere L."+
                "\nSe hai finito di leggere basta premere un pulsante!" +
                "\nBUONA FORTUNA!!!");
            Console.ReadKey(true);
            Console.Clear();

            // Quando si clicca su qualsiai tasto cki assume quel valore.
            ConsoleKeyInfo cki = new ConsoleKeyInfo();

            int tempo = 300; // Velocità del cambiamento di colore.
            string mesg = "PREMI UN PULSANTE PER INIZIARE LA BATTAGLIA"; // Messaggio iniziale dopo al regolamento.

            Console.CursorVisible = false; // Nascondere il cursore.

            // Schermata iniziale con avanzamento della velocita del cambiamento di sfondo.
            while (!Console.KeyAvailable)
            {
                // Velocizzazione del processo iniziale.
                tempo = tempo - 10;
                if (tempo == 10)
                    tempo = 300;

                Console.BackgroundColor = ConsoleColor.Green;
                Console.Clear();
                Console.SetCursorPosition((Console.WindowWidth - mesg.Length) / 2,
                                           Console.WindowHeight / 2);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(mesg);

                Thread.Sleep(tempo);

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                Console.SetCursorPosition((Console.WindowWidth - mesg.Length) / 2,
                                           Console.WindowHeight / 2);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(mesg);

                Thread.Sleep(tempo);
            }

            // Variabili astronave bassa.
            int xn = (Console.WindowWidth - 5) / 2;
            int yn = Console.WindowHeight - 4;

            // Variabili dello sparo.
            int xs = 0, ys = 0;
            bool shotRun = false;

            // Colori.
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorVisible = false;
            Console.Clear();

            // Una animazione ogni volta che si ripete il ciclo.
            bool run = true;  // Rileva la condizione d'uscita
            bool inc = true;  // 
            

            // Scrittura del livello su console e delle vite.
            int livello = 1;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, 6);
            Console.Write("Livello: " + livello);
            Console.SetCursorPosition(2, 7);
            Console.Write("Vite: " + vite);

            // Astronave nemica.
            string[] ship = { 
                " |   | ",
                "  ---  " ,
                "  --- ",
                " | V | ",
                "       "
                };

            while (run)
            {
                // Lettura della tastiera.
                if (Console.KeyAvailable)
                    cki = Console.ReadKey(true);

                // Spastamento dastra, sinistra, sparo.
                if (cki.Key == ConsoleKey.Escape)
                    run = false;
                if (cki.Key == ConsoleKey.LeftArrow && xn > 11)
                    xn--;
                if (cki.Key == ConsoleKey.RightArrow && xn < 108)
                    xn++;
                if (cki.Key == ConsoleKey.Spacebar && !shotRun)
                {
                    xs = xn + 5;
                    ys = yn - 4;
                    shotRun = true;
                    Console.Beep();// In presenza di audio, suono. 
                    primoSparo = true;
                }
                // Salita astronave amica.
                if (cki.Key == ConsoleKey.UpArrow && yn > 20)
                {
                    yn--;
                    movimento = true;
                }
                // Discesa astronave amica.
                if (cki.Key == ConsoleKey.DownArrow && yn < 30)
                {
                    yn++;
                    movimento = true;
                }
                // Possibilità di andare da un livello a un altro.
                if (cki.Key == ConsoleKey.L)
                {
                    Console.Clear();
                    SetCursorPosition(0, 0);
                    do
                    {
                        Console.Write("Inserire il livello (1-8): ");
                        ok = int.TryParse(Console.ReadLine(), out livello) && livello >= 1 && livello <= 8;
                        if (!ok)
                        {
                            Console.WriteLine("Numero del livello errato rinserirlo");
                        }
                    } while (!ok);
                    Console.Clear();

                    // Scrittura del livello.
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(2, 6);
                    Console.Write("Livello: " + livello);
                    Console.SetCursorPosition(2, 7);
                    Console.Write("Vite: " + vite);

                    // Cambiamento della velocità a seconda del livello scelto.
                    switch (livello)
                    {
                        case 1:
                            fermo = 25;
                            break;
                        case 2:
                            fermo = 23;
                            break;
                        case 3:
                            fermo = 21;
                            break;
                        case 4:
                            fermo = 19;
                            break;
                        case 5:
                            fermo = 17;
                            break;
                        case 6:
                            fermo = 15;
                            break;
                        case 7:
                            fermo = 13;
                            break;
                        case 8:
                            fermo = 11;
                            break;
                    }
                }

                // Composizione della navicella amica.
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(xn + 4, yn - 2);
                Console.WriteLine(" |        ");
                Console.SetCursorPosition(xn + 3, yn - 1);
                Console.WriteLine(" | |      ");
                Console.SetCursorPosition(xn + 2, yn);
                Console.WriteLine(" |   |    ");
                Console.SetCursorPosition(xn + 1, yn + 1);
                Console.WriteLine(" |     |   ");
                Console.SetCursorPosition(xn , yn + 2);
                Console.WriteLine(" --------- ");

                // Cancellazione dell'astronave amica si muove in alto o in basso.
                if(movimento)
                {
                    Console.SetCursorPosition(xn, yn +3);
                    Console.WriteLine("           ");
                    Console.SetCursorPosition(xn + 4, yn -3);
                    Console.WriteLine("           ");
                }

                // Sparo.
                if (shotRun)
                {
                    // Miglioramento della salita dello sparo in modo tale che non si blocchi al primo movimento di salita. 
                    if (primoSparo)
                    {
                        ys--;
                        primoSparo = false;
                    }

                    // Creazione dello sparo.
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(xs, ys + 5);
                    Console.WriteLine(" ");
                    Console.SetCursorPosition(xs, ys+4);
                    Console.WriteLine("|");
                    Console.SetCursorPosition(xs, ys+3);
                    Console.WriteLine("|");
                    Console.SetCursorPosition(xs, ys+2);
                    Console.WriteLine("|");
                    Console.SetCursorPosition(xs, ys+1);
                    Console.WriteLine("|");
                    Console.SetCursorPosition(xs, ys);
                    Console.WriteLine("^");

                    // Salita dello sparo.
                    if (ys > 0)
                    {
                        ys--;
                    }
                    // Blocco e scomparimento dello sparo.
                    else
                    {
                        shotRun = false;
                        Thread.Sleep(25);
                        Console.SetCursorPosition(xs, ys);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(xs, ys + 1);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(xs, ys + 2);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(xs, ys + 3);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(xs, ys + 4);
                        Console.WriteLine(" ");
                        Console.SetCursorPosition(xs, ys + 5);
                        Console.WriteLine(" ");

                    }

                    // Se lo sparo colpisce l'astronave nemica si sale di livello e il gioco si velocizza.
                    if (ys==1 && xs>=xa && xs<xa+7)
                    {
                        string fine = "VITTORIA!";
                        Console.Clear();
                        Console.SetCursorPosition((Console.WindowWidth - fine.Length) / 2,
                                           Console.WindowHeight / 2);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(fine);
                        Thread.Sleep(750);
                        Console.Clear();
                        livello++;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(2, 6);
                        Console.Write("Livello: "+livello);
                        Console.SetCursorPosition(2, 7);
                        Console.Write("Vite: " + vite);

                        // Modifica alla velocità di gioco all'aumentare dei livelli.
                        if (fermo > 11)
                            fermo -= 2;
                        else
                        {
                            // Chiusura del gioco al raggiungimento e alla vittoria dell'ultimo livello.
                            string sigla = "CONGRATULAZIONI HAI VINTO!";
                            string sigla1 = " Il programma si autodistruggerà tra 5 secondi...";
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.SetCursorPosition((Console.WindowWidth - sigla.Length) / 2,Console.WindowHeight / 2);
                            Console.Write(sigla);
                            Console.SetCursorPosition((Console.WindowWidth - sigla1.Length) / 2, Console.WindowHeight / 2 +1);
                            Console.Write(sigla1);
                            Thread.Sleep(5000);
                            run = false;
                        }
                    }
                }

                // Pulizia dell'input.
                cki = new ConsoleKeyInfo();
                
                // Nasconde il cursore.
                Console.CursorVisible = false;

                // Spazio e spostamento possibile della navicella alta.
                if (inc)
                {
                    xa++;
                    if (xa > 105)
                        inc = false;
                }
                else
                {
                    xa--;
                    if (xa < 13)
                        inc = true;
                }
                
                // Disegna la nave.
                Console.ForegroundColor = ConsoleColor.White;
                
                // Creazione dell astronave nemica.
                for (int i=0; i <= 4; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(xa, i);
                    Console.Write(ship[i]);
                }

                // Sparo della navicella nemica.
                if (sparo > 0)
                    sparo--;
                else
                {
                    // Reinizializzazione dopo all'attesa impostata allo sparo della navicella nemica.
                    if (vero == true)
                    {
                        xsn = xa;
                        vero = false;
                    }

                    // Sparo della navicella nemica.
                    Console.SetCursorPosition(xsn +3, ysn+1);
                    Console.WriteLine(" | ");
                    Console.SetCursorPosition(xsn+3, ysn);
                    Console.WriteLine("   ");

                    // Discesa dello sparo.
                    if(ysn<32)
                    {
                        ysn++;

                        // Caso in cui lo sparo nemico colpisca l'astronave amica.
                        if(ysn==yn && xsn>= xn-3 && xsn<=xn+6)
                        {
                            string fine = "HAI PERSO!";
                            Console.Clear();
                            Console.SetCursorPosition((Console.WindowWidth - fine.Length) / 2,
                                               Console.WindowHeight / 2);
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write(fine);
                            Thread.Sleep(750);
                            Console.Clear();

                            // Diminuzione del livello o se nel livello 1 diminuzione delle vite. 
                            if (livello > 1)
                            {
                                livello--;
                            }
                            else
                            {
                                vite--;
                            }

                            // Scrittura del livello e delle vite aggiornate.
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(2, 6);
                            Console.Write("Livello: " + livello);
                            Console.SetCursorPosition(2, 7);
                            Console.Write("Vite: " + vite);
                            fermo += 2; // Aumento del tempo in caso di perdita.

                            // Chiusura del gioco per aver finito le vite nel 1 livello.
                            if (vite == 0)
                            {
                                string sigla = "CONGRATULAZIONI HAI PERSO!";
                                string sigla1 = " Il programma si auto ditruggerà tra 5 secondi...";
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.SetCursorPosition((Console.WindowWidth - sigla.Length) / 2, Console.WindowHeight / 2);
                                Console.Write(sigla);
                                Console.SetCursorPosition((Console.WindowWidth - sigla1.Length) / 2, Console.WindowHeight / 2 + 1);
                                Console.Write(sigla1);
                                Thread.Sleep(5000);
                                run = false; // Variabile per far chiudere il programma.
                            }
                            
                        }
                    }
                    // Scomparsa dello sparo se non colpisce l'astronave.
                    else
                    {
                        Console.SetCursorPosition(xsn + 3, ysn + 1);
                        Console.WriteLine("  ");
                        vero = true;
                        ysn = 4;
                        sparo = 10;
                    }
                }
                // Attesa di tot ms e con l'aumentare dei livelli diminuisce.
                Thread.Sleep(fermo);
            }
        }
    }
}