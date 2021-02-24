using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;

namespace Andtech.MicroDatabase.Tests {

	public static class DatabaseTests {

		[Test]
		public static void TestCreate() {
			// Arrange
			var data = Substitute.For<MockData>(Guid.NewGuid());
			var database = new Database<MockData>(new GuidSet<MockData>());

			// Act
			database.Add(data);

			// Assert
			Assert.AreEqual(1, database.Count());
		}

		[Test]
		public static void TestDelete() {
			// Arrange
			var data = Substitute.For<MockData>(Guid.NewGuid());
			var database = new Database<MockData>(new GuidSet<MockData>());
			database.Add(data);

			// Act
			database.Remove(data);

			// Assert
			Assert.AreEqual(0, database.Count());
		}

		[Test]
		public static void TestCreateCallback() {
			// Arrange
			var data = Substitute.For<MockData>(Guid.NewGuid());
			var database = new Database<MockData>(new GuidSet<MockData>());
			database.Created += Database_Created;

			// Act
			bool calledback = false;
			database.Add(data);

			// Assert
			Assert.AreEqual(true, calledback);

			void Database_Created(object sender, DatabaseEventArgs<MockData> e) {
				calledback = true;
			}
		}

		[Test]
		public static void TestDeleteCallback() {
			// Arrange
			var data = Substitute.For<MockData>(Guid.NewGuid());
			var database = new Database<MockData>(new GuidSet<MockData>());
			database.Deleted += Database_Delete;
			database.Add(data);

			// Act
			bool calledback = false;
			database.Remove(data);

			// Assert
			Assert.AreEqual(true, calledback);

			void Database_Delete(object sender, DatabaseEventArgs<MockData> e) {
				calledback = true;
			}
		}

		[Test]
		public static void TestUpdateCallback() {
			// Arrange
			var data = new MockData(Guid.NewGuid());
			var database = new Database<MockData>(new GuidSet<MockData>());
			database.Updated += Database_Updated;
			database.Add(data);

			// Act
			bool calledback = false;
			data.Health = 100.0F;

			// Assert
			Assert.AreEqual(true, calledback);

			void Database_Updated(object sender, DatabaseEventArgs<MockData> e) {
				calledback = true;
			}
		}
	}
}
