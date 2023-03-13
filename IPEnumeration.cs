namespace ScanIpv4;

internal class IPEnumeration: IEnumerable
{
	private string startAddress;
	private string endAddress;

	internal static long AddressToInt(IPAddress addr)
	{
		byte[] addressBits = addr.GetAddressBytes();

		long retval = 0;
		for (int i = 0; i < addressBits.Length; i++)
		{
			retval = (retval << 8) + (int)addressBits[i];
		}

		return retval;
	}

	internal static long AddressToInt(string addr)
	{
		return AddressToInt(IPAddress.Parse(addr));
	}

	internal static IPAddress IntToAddress(long addr)
	{
		return IPAddress.Parse(addr.ToString());
	}


	public IPEnumeration(string startAddress, string endAddress)
	{
		this.startAddress = startAddress;
		this.endAddress = endAddress;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return (IEnumerator)GetEnumerator();
	}

	public IPEnumerator GetEnumerator()
	{
		return new IPEnumerator(startAddress, endAddress);
	}
}
