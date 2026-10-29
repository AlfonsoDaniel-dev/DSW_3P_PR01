namespace dsw_3p_pr01.Repositories;

using dsw_3p_pr01.Models; 

using System.Text.Json;

public class MissionJSONRepository : IMissionRepository{

    private readonly string _filePath = "";

    private readonly JsonSerializerOptions  _options = new JsonSerializerOptions {
        WriteIndented = true,
    };


    public MissionJSONRepository(string filePath) {
        _filePath = Path.Join(Directory.GetCurrentDirectory(), "data", "missions.json");

        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

        if (!File.Exists(_filePath)) {
            File.WriteAllText(_filePath, "[]");
        }
    }

    public List<Mission> Read() {
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Mission>>(json, _options)!;
    }

    public void Write(List<Mission> missions) {
        var json = JsonSerializer.Serialize(missions, _options);
        File.WriteAllText(_filePath, json);
    }

    public List<Mission> GetAll() {
        return Read();
    }

    public Mission? GetById(int id)
    {
        var Mission = Read().FirstOrDefault(m => m.Id == id);
        if (Mission == null)
        {
            return null;
        }

        return Mission;
    }

    public bool Create(Mission mission) {

        mission.Id = Read().Count > 0 ? Read().Max(m => m.Id) + 1 : 1;
        var missions = Read();
        missions.Add(mission);

        Write(missions);
        return true;
    }

    public bool Update(int id, Mission mission) {

        var missions = Read();
        var quest = missions.FirstOrDefault(m => m.Id == id);
        if (quest == null) {
            return false;
        }

        quest.Name = mission.Name;
        quest.Difficulty = mission.Difficulty;
        quest.ExperienceReward = mission.ExperienceReward;
        quest.completed = mission.completed;

        Write(missions);
        return true;
    }

    public bool Delete(int id) {

        var missions = Read();
        var mission = missions.FirstOrDefault(m => m.Id == id);
        if (mission == null) {
            return false;
        }

        missions.Remove(mission);
        Write(missions);
        return true;
    }






}