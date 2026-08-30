using Pinta.Domain.Auth;

namespace Pinta.Domain.Security;

public class Ban
{
    #region Private

    private int id;
    private string reason = "";
    private DateTime startDate;
    private DateTime? endDate;
    private User user = null!;

    #endregion

    #region Public

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Reason
    {
        get { return reason; }
        set { reason = value; }
    }

    public DateTime StartDate
    {
        get { return startDate; }
        set { startDate = value; }
    }

    public DateTime? EndDate
    {
        get { return endDate; }
        set { endDate = value; }
    }

    public virtual User User
    {
        get { return user; }
        set { user = value; }
    }

    #endregion
}