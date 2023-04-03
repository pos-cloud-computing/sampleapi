namespace Domain.Common.Exception
{
	public class Message
	{
		//Core 
		public const string SKY_CORE_EMPTY_ID = "Recurso não Localizado.";
		public const string SKY_CORE_NULL_EMPTY_CODE = "Código de Mensagem não pode Ser nulo.";
		public const string SKY_CORE_NULL_EMPTY_DESCRIPTION = "Descrição não pode ser vazia.";
		public const string SKY_CORE_MAX_LENGTH_CODE = "Código da Mensagem não pode passar de 50 caracteres.";
		public const string SKY_CORE_MAX_LENGTH_MESSAGEM_API_DESCRIPTION = "Descrição da Mensagem não pode passar de 1000 caracteres.";

		//Sample 
		public const string SKY_EXAMPLE_EMPTY_NULL_NAME = "Nome do Exemplo não pode ser vazio ou nulo.";
		public const string SKY_EXAMPLE_MAX_LENGTH_30_NAME = "Nome do Exemplo não pode passar de 30 caracteres.";
		public const string SKY_EXAMPLE_EXISTING = "Exemplo existente.";
		public const string SKY_EXAMPLE_NOT_FOUND_SAMPLE = "Exemplo Não encontrado.";
		public const string SKY_EXAMPLE_NOT_FOUND = "Exemplo Não Localizado.";
	}
}
