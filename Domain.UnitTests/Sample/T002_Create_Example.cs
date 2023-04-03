using Domain.Common.Exception;
using Domain.Sample.Repository;
using Domain.Sample.Service;
using Moq;

namespace Domain.UnitTests.Sample
{
	public class T002_Create_Example
	{
		public readonly Mock<IExampleRepository> _sampleRepository = new();
		public ExampleDomainService? _sampleDomainService;

		[Theory]
		[InlineData("Exemplo 12")]
		public void Example_Sample_Valid(string name)
		{
			// Arrange
			_sampleRepository.Setup(x => x.GetByName(name));
			_sampleDomainService = new ExampleDomainService(_sampleRepository.Object);
			var exampleDomain = new Domain.Sample.Entity.Example(name);

			//Act
			_sampleDomainService.Create(exampleDomain);

			//Assert
			Assert.NotNull(exampleDomain);
		}

		[Theory]
		[InlineData(null)]
		public void Create_Example_Invalid_Null(Domain.Sample.Entity.Example example)
		{
			// Arrange
			_sampleDomainService = new ExampleDomainService(_sampleRepository.Object);

			//Act
			var ex = Assert.Throws<NotFoundException>(() => _sampleDomainService.Create(example));

			//Assert
			Assert.Equal(Message.SKY_EXAMPLE_NOT_FOUND, ex.Message);
		}

		[Theory]
		[InlineData("Exemplo 12")]
		public void Example_Invalid_Existing_Name(string name)
		{
			// Arrange
			_sampleRepository.Setup(x => x.GetByName(name)).Returns(new Domain.Sample.Entity.Example(12, "Exemplo 12", true));
			_sampleDomainService = new ExampleDomainService(_sampleRepository.Object);
			var exampleDomain = new Domain.Sample.Entity.Example(name);

			//Act
			var ex = Assert.Throws<BusinessException>(() => _sampleDomainService.Create(exampleDomain));

			//Assert
			Assert.Equal(Message.SKY_EXAMPLE_EXISTING, ex.Message);
		}
	}
}
