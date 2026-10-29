namespace dsw_3p_pr01.Repositories;

using dsw_3p_pr01.Models;

public interface IMissionRepository {
    public List<Mission> GetAll();
    public Mission? GetById(int id);
    public bool Create(Mission mission);
    public bool Update(int id, Mission mission);
    public bool Delete(int id);
}