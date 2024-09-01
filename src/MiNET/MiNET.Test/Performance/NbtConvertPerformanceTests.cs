using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using fNbt;
using fNbt.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiNET.Items;

namespace MiNET.Test.Performance
{
	[TestClass]
	[Ignore("only for manual run")]
	public class NbtConvertPerformanceTests : PerformanceTestBase
	{
		private static TestObject _testObj;
		private static MemoryStream _testStream;
		private static NbtFile _testTag;

		private static Item _item;
		private static NbtTag _itemTag;

		static NbtConvertPerformanceTests()
		{
			NbtSerializerSettings.DefaultSettings.LoopReferenceHandling = fNbt.Serialization.Handlings.LoopReferenceHandling.Serialize;

			_testObj = new TestObject()
			{
				TestByte = 131,
				TestShort = 13141,
				TestInt = 131412,
				TestLong = 13141231212,
				TestFloat = 123.21f,
				TestDecimal = 123.21,
				TestByteArray = new byte[] { 14, 2, 121 },
				TestIntArray = new int[] { 14, 2, 121 },
				TestLongArray = new long[] { 14, 2, 121 },
				TestSubObj = new TestObject.SubObject() { TestSubString = "test value" },
				TestDictionary = new Dictionary<string, TestObject.SubObject>() {
					{ "test key 0", new TestObject.SubObject() { TestSubString = "test value" } },
					{ "test key 1", new TestObject.SubObject() { TestSubString = "test new value" } }
				},
				TestList = new List<TestObject.SubObject>() {
					new TestObject.SubObject() { TestSubString = "test val" }
				}
			};

			_testStream = new MemoryStream();

			NbtSerializer.Write(_testObj, _testStream);
			_testStream.Position = 0;

			_testTag = new NbtFile();
			_testTag.LoadFromStream(_testStream, NbtCompression.None);
			_testStream.Position = 0;

			var item = new ItemPrismarineBrickSlab();
			item.Block.VerticalHalf = "top";
			_itemTag = NbtConvert.ToNbt(_item = item);
		}

		[Benchmark]
		public void ItemToNbt()
		{
			NbtConvert.ToNbt(_item);
		}

		[Benchmark]
		public void ItemFromNbt()
		{
			NbtConvert.FromNbt<Item>(_itemTag);
		}

		[Benchmark]
		public void TagsWrite()
		{
			_testTag.SaveToStream(new MemoryStream(), NbtCompression.None);
		}

		[Benchmark]
		public void TagsRead()
		{
			_testStream.Position = 0;

			_testTag = new NbtFile();
			_testTag.LoadFromStream(_testStream, NbtCompression.None);
		}

		[Benchmark]
		public void ObjectWrite()
		{
			NbtSerializer.Write(_testObj, new MemoryStream());
		}

		[Benchmark]
		public void ObjectRead()
		{
			_testStream.Position = 0;

			NbtSerializer.Read<TestObject>(_testStream);
		}

		[Benchmark]
		public void ConvertObjectToTag()
		{
			NbtConvert.ToNbt(_testObj);
		}

		[Benchmark]
		public void ConvertTagToObject()
		{
			NbtConvert.FromNbt<TestObject>(_testTag);
		}

		[NbtObject]
		public class TestObject
		{
			public byte TestByte { get; set; }
			public short TestShort { get; set; }

			public int TestInt { get; set; }

			public long TestLong { get; set; }

			[NbtProperty("test_float")]
			public float TestFloat { get; set; }

			public double TestDecimal { get; set; }

			public byte[] TestByteArray { get; set; }

			public int[] TestIntArray { get; set; }

			public long[] TestLongArray { get; set; }

			public SubObject TestSubObj { get; set; }

			public Dictionary<string, SubObject> TestDictionary { get; set; }

			public List<SubObject> TestList { get; set; }

			public override bool Equals(object obj)
			{
				return obj is TestObject @object &&
					   TestByte == @object.TestByte &&
					   TestShort == @object.TestShort &&
					   TestInt == @object.TestInt &&
					   TestLong == @object.TestLong &&
					   TestFloat == @object.TestFloat &&
					   TestDecimal == @object.TestDecimal &&
					   TestByteArray.SequenceEqual(@object.TestByteArray) &&
					   TestIntArray.SequenceEqual(@object.TestIntArray) &&
					   TestLongArray.SequenceEqual(@object.TestLongArray) &&
					   EqualityComparer<SubObject>.Default.Equals(TestSubObj, @object.TestSubObj) &&
					   TestDictionary.SequenceEqual(@object.TestDictionary) &&
					   TestList.SequenceEqual(@object.TestList);
			}

			public override int GetHashCode()
			{
				HashCode hash = new HashCode();
				hash.Add(TestByte);
				hash.Add(TestShort);
				hash.Add(TestInt);
				hash.Add(TestLong);
				hash.Add(TestFloat);
				hash.Add(TestDecimal);
				hash.Add(TestByteArray);
				hash.Add(TestIntArray);
				hash.Add(TestLongArray);
				hash.Add(TestSubObj);
				hash.Add(TestDictionary);
				hash.Add(TestList);
				return hash.ToHashCode();
			}

			[NbtObject]
			public class SubObject
			{
				public string TestSubString { get; set; }

				public override bool Equals(object obj)
				{
					return obj is SubObject @object &&
						   TestSubString == @object.TestSubString;
				}

				public override int GetHashCode()
				{
					return HashCode.Combine(TestSubString);
				}
			}

			[NbtObject]
			public class LoopObject
			{
				public LoopObject Loop { get; set; }
			}
		}
	}
}
