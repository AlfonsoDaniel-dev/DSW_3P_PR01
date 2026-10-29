namespace dsw_3p_pr01.Services;

using dsw_3p_pr01.Models;
using dsw_3p_pr01.Repositories;

public interface IMissionService {
    public List<Mission> GetAll();
    public Mission? GetById(int id);
    public Mission? Create(Mission mission);
    public bool Update(int id, Mission mission);
    public bool Delete(int id);
}

public class MissionService : IMissionService {

    private readonly IMissionRepository _repository;

    public MissionService(IMissionRepository repository) {
        _repository = repository;
    }

    public List<Mission> GetAll() {
        return _repository.GetAll();
    }

    public Mission? GetById(int id) {
        return _repository.GetById(id);
    }

    public Mission? Create(Mission mission) {
        if (!_repository.Create(mission)) {
            return null;
        }

        return mission;
    }

    public bool Update(int id, Mission mission) {
        return _repository.Update(id, mission);
    }

    public bool Delete(int id) {
        return _repository.Delete(id);
    }
}
