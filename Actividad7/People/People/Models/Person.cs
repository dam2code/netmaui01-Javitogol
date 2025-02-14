using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace People.Models;

[SQLite.Table("people")]
public class Person
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [SQLite.MaxLength(250), Unique]
    public string Name { get; set; }
}