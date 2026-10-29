namespace dsw_3p_pr01.Repositories;

using dsw_3p_pr01.Models;

public interface IMissionRepository {
    public List<Mission> GetAll();
    public Mission GetById();
    public bool Create(Mission missionp);
    public bool Update(int id, Mission mission);
    public bool Delete(int id);
}