using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities;

public class Notification
{
    [NotMapped]
    public string PropName { get; set; }
    [NotMapped]
    public string Message { get; set; }
    [NotMapped]
    public List<Notification> notifications;

    public Notification()
    {
        notifications = new List<Notification>();
    }

    public bool ValidateStringProperty(string value, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(propertyName))
        {
            notifications.Add(new Notification
            {
                PropName = propertyName,
                Message = "Campo Obrigatório: "
            });
            return false;
        }
        return true;
    }

    public bool ValidateIntProperty(int value, string propertyName)
    {
        if (value < 1 || string.IsNullOrWhiteSpace(propertyName))
        {
            notifications.Add(new Notification
            {
                PropName = propertyName,
                Message = "Campo Obrigatório: "
            });
            return false;
        }
        return true;
    }
}
