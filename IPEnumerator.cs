namespace ScanIpv4;

public class IPEnumerator : IEnumerator
{
	private string startAddress;
	private string endAddress;
	private long currentIP;
	private long endIP;

	public IPEnumerator(string startAddress, string endAddress)
	{
		this.startAddress = startAddress;
		this.endAddress = endAddress;

		currentIP = IPEnumeration.AddressToInt(startAddress);
		endIP = IPEnumeration.AddressToInt(endAddress);
	}

	public bool MoveNext()
	{
		currentIP++;
		return (currentIP <= endIP);
	}

	public void Reset()
	{
		currentIP = IPEnumeration.AddressToInt(startAddress);
	}

	object IEnumerator.Current
	{
		get
		{
			return Current;
		}
	}

	public IPAddress Current
	{
		get
		{
			try
			{
				return IPEnumeration.IntToAddress(currentIP);
			}
			catch (IndexOutOfRangeException)
			{
				throw new InvalidOperationException();
			}
		}
	}
}
