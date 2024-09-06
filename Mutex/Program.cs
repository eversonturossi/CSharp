Console.WriteLine("Hello, World!");

if (!mutexInstancia.WaitOne(TimeSpan.FromSeconds(3), false))
{
    Console.WriteLine("Outra instância já esta sendo executada");
    return;
}

try
{
    Execute();
}
finally
{
    mutexInstancia.ReleaseMutex();
}

public partial class Program
{
    private static int sharedVariable = 0;
    private static Mutex mutexThread = new Mutex();
    private static Mutex mutexInstancia = new Mutex(false, "Nome_do_Mutex");

    static void Execute()
    {
        int numThreads = 50;
        Thread[] threads = new Thread[numThreads];

        for (int i = 0; i < numThreads; i++)
        {
            threads[i] = new Thread(IncrementSharedVariable!);
            threads[i].Start(i);
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine($"Valor final da variável compartilhada: {sharedVariable}");
    }

    static void IncrementSharedVariable(object threadId)
    {
        for (int i = 0; i < 10000; i++)
        {
            mutexThread.WaitOne(); // Aguarde o mutex para garantir exclusão mútua
            try
            {
                sharedVariable++;
            }
            finally
            {
                mutexThread.ReleaseMutex(); // Libere o mutex
            }
        }
        Console.WriteLine($"Thread {threadId} concluída.");
    }
}