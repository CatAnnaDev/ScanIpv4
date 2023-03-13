
namespace ScanIpv4;

public static partial class IPHelper
{
	[LibraryImport("iphlpapi.dll")]
	public static partial int SendARP(int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);

	public static string getMAC(IPAddress address)
	{
		int intAddress = BitConverter.ToInt32(address.GetAddressBytes(), 0);

		byte[] macAddr = new byte[6];
		uint macAddrLen = (uint)macAddr.Length;
		if (SendARP(intAddress, 0, macAddr, ref macAddrLen) != 0)
			return "(NO ARP result)";

		string[] str = new string[(int)macAddrLen];
		for (int i = 0; i < macAddrLen; i++)
			str[i] = macAddr[i].ToString("x2");

		return string.Join(":", str);
	}

	public static string getPort(IPAddress address)
	{
		StringBuilder port = new();
		foreach (int s in Ports)
		{
			using (TcpClient Scan = new TcpClient())
			{
				try
				{
					Scan.Connect(address, s);
					port.Append(s +", ");
				}
				catch{}
			}
		}
		return port.ToString();
	}

	private static int[] Ports = new int[]
	{
		7,20,21,22,23,25,53,69,80,102,110,119,123,135,137,139,143,161,194,381,383,443,464,465,587,636,691,902,989,990,993,995,1025,1194,1337,1589,1725,2082,2083,2483,2484,2967,3074,3306,3724,4664,5432,5900,6665,6669,6881,6999,6970,8086,8087,8222,9100,10000,12345,27374,18006
	};
}
