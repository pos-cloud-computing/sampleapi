namespace Application.DTO.Base
{
	public interface IParser<O, D>
	{
		D Parse(O origin);

		List<D> Parse(List<O> origin);
	}
}
