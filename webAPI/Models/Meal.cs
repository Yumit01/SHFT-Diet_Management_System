public class Meal
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Description { get; set; }
    public int DietPlanId { get; set; }
    public DietPlan DietPlan { get; set; }
}
