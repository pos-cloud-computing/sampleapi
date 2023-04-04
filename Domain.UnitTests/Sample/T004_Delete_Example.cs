using Domain.Common.Exception;
using Domain.Sample.Repository;
using Domain.Sample.Service;
using Moq;

namespace Domain.UnitTests.Sample
{
	public class T004_Delete_Example
	{
		public readonly Mock<IExampleRepository> _sampleRepository = new();
		public ExampleDomainService? _sampleDomainService;

		[Theory]
		[InlineData(1, "Exemplo Update", true)]
		public void Delete_Example_Valid(long id, string name, bool active)
		{
			// Arrange
			_sampleRepository.Setup(x => x.Get(1)).Returns(new Domain.Sample.Entity.Example(1, "Exemplo 1", true));
			_sampleDomainService = new ExampleDomainService(_sampleRepository.Object);
			var exampleDomain = new Domain.Sample.Entity.Example(id, name, active);

			//Act
			_sampleDomainService.Delete(exampleDomain);

			//Assert
			Assert.False(exampleDomain.Active);
		}

		[Theory]
		[InlineData(null)]
		public void Delete_Sample_Invalid_Null(Domain.Sample.Entity.Example example)
		{
			// Arrange
			_sampleDomainService = new ExampleDomainService(_sampleRepository.Object);

			//Act
			var ex = Assert.Throws<NotFoundException>(() => _sampleDomainService.Create(example));

			//Assert
			Assert.Equal(Message.SKY_EXAMPLE_NOT_FOUND, ex.Message);
		}
	}
}
