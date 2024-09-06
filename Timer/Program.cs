
using System;
using System.Threading;
using System.Threading.Tasks;

Console.WriteLine("App iniciado");
Execute();
Console.WriteLine("App finalizado");

// Exemplo 1
// public partial class Program
// {
//     static void Execute()
//     {
//         // Cria um timer periódico que chama a função TimerCallback a cada 1000 milissegundos (1 segundo).
//         Timer timer = new Timer(TimerCallback, null, 0, 1000);

//         Console.WriteLine("Pressione Enter para parar o timer.");
//         Console.ReadLine();
//     }

//     private static void TimerCallback(object o)
//     {
//         Console.WriteLine("TimerCallback: " + DateTime.Now);
//     }
// }

// Exempo 2
public partial class Program
{
    private static Timer timer;

    public static void Execute()
    {
        var timerState = new TimerState { Counter = 0 };
        timer = new Timer(callback: new TimerCallback(TimerTask), state: timerState, dueTime: 10000, period: 1000);
        while (timerState.Counter <= 10)
        {
            Task.Delay(100).Wait();
        }

        timer.Dispose();
        Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff}: done.");
    }

    private static void TimerTask(object timerState)
    {
        Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff}: starting a new callback.");
        var state = timerState as TimerState;
        Interlocked.Increment(ref state.Counter);
    }

    class TimerState
    {
        public int Counter;
    }
}
