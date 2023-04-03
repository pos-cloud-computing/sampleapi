using Domain.Common.Exception;
using Domain.Sample.Entity;
using Domain.Sample.Repository;
using Domain.Sample.Service;
using Moq;

namespace Domain.UnitTests.Sample
{
	public class T003_Update_Example
	{
		public readonly Mock<IExampleRepository> _sampleRepository = new();
		public ExampleDomainService? _sampleDomainService;

		[Theory]
		[InlineData(1, "Exemplo Update", true)]
		public void Update_Example_Valid(long id, string name, bool active)
		{
			// Arrange
			_sampleRepository.Setup(x => x.GetByName(name)).Returns(new Domain.Sample.Entity.Example(1, "Exemplo 1", true));
			_sampleDomainService = new ExampleDomainService(_sampleRepository.Object);
			var exampleDomain = new Domain.Sample.Entity.Example(id, name, active);
			exampleDomain.UpdateName(name);

			//Act
			_sampleDomainService.Update(exampleDomain);

			//Assert
			Assert.NotNull(exampleDomain);
		}

		[Theory]
		[InlineData(null)]
		public void Update_Sample_Invalid_Null_NotFound(Example example)
		{
			// Arrange
			_sampleDomainService = new ExampleDomainService(_sampleRepository.Object);

			//Act
			var ex = Assert.Throws<BusinessException>(() => _sampleDomainService.Update(example));

			//Assert
			Assert.Equal(Message.SKY_EXAMPLE_NOT_FOUND_SAMPLE, ex.Message);
		}

		[Theory]
		[InlineData("Exemplo Update")]
		public void Update_Sample_Invalid_Id_NotFound(string name)
		{
			// Arrange
			_sampleDomainService = new ExampleDomainService(_sampleRepository.Object);
			var exampleDomain = new Domain.Sample.Entity.Example(name);

			//Act
			var ex = Assert.Throws<BusinessException>(() => _sampleDomainService.Update(exampleDomain));

			//Assert
			Assert.Equal(Message.SKY_EXAMPLE_NOT_FOUND_SAMPLE, ex.Message);
		}
	}
}
