using System;

namespace Domainr
{
    public static partial class Domainr
	{

		public static Tuple<T1, T2> GetValues<T1, T2>(string key1, string key2)
		{
			var currentDomain = AppDomain.CurrentDomain;

			var value1 = (T1) currentDomain.GetData(key1);
			var value2 = (T2) currentDomain.GetData(key2);
						
			return Tuple.Create(value1, value2);
		}

		public static Tuple<T1, T2, T3> GetValues<T1, T2, T3>(string key1, string key2, string key3)
		{
			var currentDomain = AppDomain.CurrentDomain;

			var value1 = (T1) currentDomain.GetData(key1);
			var value2 = (T2) currentDomain.GetData(key2);
			var value3 = (T3) currentDomain.GetData(key3);
						
			return Tuple.Create(value1, value2, value3);
		}

		public static Tuple<T1, T2, T3, T4> GetValues<T1, T2, T3, T4>(string key1, string key2, string key3, string key4)
		{
			var currentDomain = AppDomain.CurrentDomain;

			var value1 = (T1) currentDomain.GetData(key1);
			var value2 = (T2) currentDomain.GetData(key2);
			var value3 = (T3) currentDomain.GetData(key3);
			var value4 = (T4) currentDomain.GetData(key4);
						
			return Tuple.Create(value1, value2, value3, value4);
		}

		public static Tuple<T1, T2, T3, T4, T5> GetValues<T1, T2, T3, T4, T5>(string key1, string key2, string key3, string key4, string key5)
		{
			var currentDomain = AppDomain.CurrentDomain;

			var value1 = (T1) currentDomain.GetData(key1);
			var value2 = (T2) currentDomain.GetData(key2);
			var value3 = (T3) currentDomain.GetData(key3);
			var value4 = (T4) currentDomain.GetData(key4);
			var value5 = (T5) currentDomain.GetData(key5);
						
			return Tuple.Create(value1, value2, value3, value4, value5);
		}

		public static Tuple<T1, T2, T3, T4, T5, T6> GetValues<T1, T2, T3, T4, T5, T6>(string key1, string key2, string key3, string key4, string key5, string key6)
		{
			var currentDomain = AppDomain.CurrentDomain;

			var value1 = (T1) currentDomain.GetData(key1);
			var value2 = (T2) currentDomain.GetData(key2);
			var value3 = (T3) currentDomain.GetData(key3);
			var value4 = (T4) currentDomain.GetData(key4);
			var value5 = (T5) currentDomain.GetData(key5);
			var value6 = (T6) currentDomain.GetData(key6);
						
			return Tuple.Create(value1, value2, value3, value4, value5, value6);
		}

		public static Tuple<T1, T2, T3, T4, T5, T6, T7> GetValues<T1, T2, T3, T4, T5, T6, T7>(string key1, string key2, string key3, string key4, string key5, string key6, string key7)
		{
			var currentDomain = AppDomain.CurrentDomain;

			var value1 = (T1) currentDomain.GetData(key1);
			var value2 = (T2) currentDomain.GetData(key2);
			var value3 = (T3) currentDomain.GetData(key3);
			var value4 = (T4) currentDomain.GetData(key4);
			var value5 = (T5) currentDomain.GetData(key5);
			var value6 = (T6) currentDomain.GetData(key6);
			var value7 = (T7) currentDomain.GetData(key7);
						
			return Tuple.Create(value1, value2, value3, value4, value5, value6, value7);
		}

				
	}
}