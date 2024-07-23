using System.ComponentModel.DataAnnotations;

namespace DigitalBankDDD.Domain.Entities;

public class BaseEntity
{
    [Key]
    public int Id { get; private set; }
}