namespace ScanIpv4;

internal class Program
{
	static int ThreadCount = 20;
	private static List<Thread> threadList = new();
	private static Queue<string> ipList = new();
	private static bool scanPort = false;

	static void Main(string[] args)
	{
		foreach (IPAddress addr in new IPEnumeration("192.168.1.0", "192.168.1.254"))
			ipList.Enqueue(addr.ToString());

		SpawnThreads();
		Console.ReadKey(true);
	}

	private static void SpawnThreads()
	{
		for (int i = 0; i <= ThreadCount; i++)
		{
			Thread thread1 = new Thread(Worker);
			threadList.Add(thread1);
			thread1.Start();
		}
	}

	private static void Worker()
	{
		while (ipList.Count != 0)
		{
			try
			{
				var ip = ipList.Dequeue();
				var mac = IPHelper.getMAC(IPAddress.Parse(ip));
				if (mac != "(NO ARP result)" && scanPort)
					Console.WriteLine("{0}\t{1}\t{2}", ip, mac, IPHelper.getPort(IPAddress.Parse(ip)));
				else if(mac != "(NO ARP result)" && !scanPort)
					Console.WriteLine("{0}\t{1}", ip, mac);
			}
			catch { }
		}
	}
}