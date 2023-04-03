using Domain.Common.Exception;

namespace Domain.UnitTests.Sample
{
	public class T001_Example
	{
		[Theory]
		[InlineData("Exemplo 12")]
		[InlineData("Exemplo 13")]
		public void Example_Valid(string name)
		{
			//Act
			var sampleDomain = new Domain.Sample.Entity.Example(name);

			//Assert
			Assert.NotNull(sampleDomain);
		}

		[Theory]
		[InlineData(1, "Exemplo 12", true)]
		[InlineData(2, "Exemplo 12", true)]
		public void Example_Existing_Valid(long id, string name, bool active)
		{
			//Act
			var exampleDomain = new Domain.Sample.Entity.Example(id, name, active);

			//Assert
			Assert.NotNull(exampleDomain);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Example_Invalid_Empty_Null_Name(string name)
		{
			//Act
			var ex = Assert.Throws<BusinessException>(() => new Domain.Sample.Entity.Example(name));

			//Assert
			Assert.Equal(Message.SKY_EXAMPLE_EMPTY_NULL_NAME, ex.Message);
		}

		[Theory]
		[InlineData("SKY_MSG_EMPTY_ID_SKY_MSG_EMPTY_ID_SKY_MSG_EMPTY_SSSS")]
		[InlineData("SKY_MSG_EMPTY_ID_SKY_MSG_EMPTY_ID_SKY_MSG_EMPTY_ID_SKY_MSG_EMPTY_ID")]
		public void Example_Invalid_Max_Length_Name(string name)
		{
			//Act
			var ex = Assert.Throws<BusinessException>(() => new Domain.Sample.Entity.Example(name));

			//Assert
			Assert.Equal(Message.SKY_EXAMPLE_MAX_LENGTH_30_NAME, ex.Message);
		}

		[Theory]
		[InlineData(0, "Exemplo 12", true)]
		public void Example_Invalid_Empty_Null_Id(long id, string name, bool active)
		{
			//Act
			var ex = Assert.Throws<NotFoundException>(() => new Domain.Sample.Entity.Example(id, name, active));

			//Assert
			Assert.Equal(Message.SKY_CORE_EMPTY_ID, ex.Message);
		}
	}
}
