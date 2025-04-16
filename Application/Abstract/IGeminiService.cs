using System.Threading.Tasks;

namespace Application.Abstract
{
	public interface IGeminiService
	{
		Task<string> SuggestContinuationAsync(string input, string currentContent = "");
		void ClearConversationHistory();
	}
}