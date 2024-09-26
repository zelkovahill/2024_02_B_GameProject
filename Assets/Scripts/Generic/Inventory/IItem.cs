// 모든 아이템의 기본 인터페이스
// 메소드 이벤트 인덱서 프로퍼티
// 모든이 무조건 public 으로 선언
// 구현부 X
public interface IItem
{
    string Name { get; }
    int ID { get; }

    void Use();

}