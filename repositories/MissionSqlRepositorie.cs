namespace dsw_3p_pr01.Repositories;

using dsw_3p_pr01.Models;
using dsw_3p_pr01.Data;

public class MissionSqlRepository : IMissionRepository
{
    
    private readonly AppDBContext _context;

    public MissionSqlRepository(AppDBContext context)
    {
        _context = context;
    }

    public List<Mission> GetAll()
    {
        return _context.Missions.ToList();
    }

    public Mission? GetById(int id)
    {
        return _context.Missions.Find(id);
    }

    public bool Create(Mission mission)
    {
        mission.Id = 0;

        _context.Missions.Add(mission);
        _context.SaveChanges();
        return true;
    }

    public bool Update(int id, Mission mission)
    {
        var existingMission = _context.Missions.Find(id);
        if (existingMission == null)
        {
            return false;
        }

        existingMission.Name = mission.Name;
        existingMission.Difficulty = mission.Difficulty;
        existingMission.ExperienceReward = mission.ExperienceReward;
        existingMission.completed = mission.completed;

        _context.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var mission = _context.Missions.Find(id);
        if (mission == null)
        {
            return false;
        }

        _context.Missions.Remove(mission);
        _context.SaveChanges();
        return true;
    }
}