# CurationAssistant
Steem Curation Assistant to provide curators helpful validation rules before submitting posts to curation group upvote

## Prerequisites
You need the following components installed on your machine in order to checkout and build:
- .NET Framework 4.7.2
- ASP.NET MVC 5
- Visual Studio (any version)

Make sure you restore the NuGet packages. By default it is enabled when you rebuild the solution.

Check out the following web.config variables, if you want to retrieve more/less data:
```xml
    <add key="HostName" value="https://api.steemit.com" />
    <add key="HistoryTransactionLimit" value="3000" />
    <add key="CommentTransactionCount" value="10" />
    <add key="PostTransactionCount" value="10" />
    <add key="VoteTransactionCount" value="10" />
    <add key="PostsMinDaysUntil" value="60" />
    <add key="CommentsMinDaysUntil" value="60" />
```
## Quick-Start
1. Clone the repository with GitHub desktop or Visual Studio GitHub plugin
2. Open Visual Studio with Administrator account (if possible0
3. Rebuild the solution
4. Go to http://localhost to view the page

In case you are not allowed to open Visual Studio with Administrator rights, you can change the project to use IISExpress instead of LocalIIS by:
- Right-clicking the project CurationAssistant in Visual Studio, 
- Click properties
- Click Web tab
- Select IIS Express from Servers section and Apply changes

## IMPORTANT NOTE
```
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
```
