# workdo-mailbox
<strong>Tuần 1: 13/3/2023 - 18/3/2023</strong>
  <h4>Công việc thực hiện được: </h4>
    <p> 1. UI trang quản lí mail ( các screen của mail bao gồm: thư đến, thư đã gửi, thư quan trọng, thư nháp, thùng rác ), chọn, hủy chọn 1 hoặc nhiều mail </p>
    <p> 2. UI phần tạo mail, tạo poll, chữ ký, view poll </p>
    <p> 3. Thiết lập:  UI, phần quản lí nhóm: xem được tất cả nhóm, tạo nhóm, thêm thành viên, edit nhóm </p>
    <p> 4. Xem chi tiết mail bao gồm: Ui, xem poll, xem thống kê </p>
<strong>Tuần 2: 20/3/2023 - 25/3/2023</strong>
  <h4>Công việc thực hiện được: </h4>
    <p> 1. Navigation, Hộp thư đến, Hộp thư đi, Hộp thư quan trọng, Thư nháp, Thùng rác, Thư theo nhãn (lọc thư theo nhãn)</p>
    <p> 2. Tạo thư</p>
    <p> 3. Quản lí nhóm: CRUD nhóm, CRUD Member khỏi nhóm</p>
    <p> 4. Quản lí nhãn: CRUD nhãn</p>
    <p> 5. Quản lí danh mục: Admin có thể CRUD danh mục</p>
    <p> 6. Quản lí chữ ký: Set chữ ký mặc định, lấy chữ ký mặc định, tạo chữ ký</p>
    <p> 7. Chi tiết thư: Check được người nhận, người gửi, trả lời được mail</p>
    <p> 8. UI gần như bám sát với design</p>
    <p> 9. Tái cấu trúc source code: Mỗi chức năng được đưa vào trong 1 folder, component sẽ có "_" phía trước, các component dùng chung được bỏ ra ngoài</p>
    <p> 10. Tạo được poll</p>
    <p> 12.Trong folder helper có 1 file là: "InitDataFakeHelper.cs" file này dùng để fake data</p>
    <p> 13. Đã viết collections cần thiết cho dự án tuần sau sẽ bắt đầu làm việc với database, các model </p>
    
<strong>Tuần 3: 27/3/2023 - 1/4/2023</strong>
  <h4>Công việc thực hiện được: </h4>
    <p> 1. CRUD "Chữ ký", "Nhóm", "Thành viên trong nhóm", "Nhãn", "Danh mục" (MongoDB) </p>
    <p> 2. Tạo, xem được mail, đấy file lên, dowload file về trong mail </p>
    <p> 3. Check được người gửi, người nhận, check được author mail, filter được mail gửi mail, mail nhận, mail quan trọng,... (MongoDB) </p>
    <p> 4. CRUD Poll, xem thống kế poll, vote poll (MongoDB) </p>
    <p> 5. Login với, check login, check current user,... </p>
    <p> 6. Hoàn thiện tất cả UI </p>
    <p> 7. Clean code, sửa lại src cho phù hợp với yêu cầu </p>

<strong>Tuần 4: 3/4/2023 - 8/4/2023</strong>
  <h4>Công việc thực hiện được: </h4>
    <p> 1. Hoàn thiện tất cả chức năng: "Chữ ký", "Nhóm", "Thành viên trong nhóm", "Nhãn", "Danh mục" trong "Cấu hình" + "Settings" </p>
    <p> 2. Về phần mail: Hoàn thành chức năng gửi mail (gửi cc, to, bcc), chọn danh mục, chữ ký mặc định, tải file, thêm xóa nhãn </p>
    <p> 3. Tạo được "poll" vote được poll, xem được phần trăm %, người đã vote cũng như chưa vote </p>
    <p> 4. Lưu được thư nháp </p>
    <p> 5. Về trang home: Đã làm được việc lọc mail theo thư đã gửi, thư đến, thư quan trọng, thư nháp, thùng rác </p>
    <p> 6. Xóa được 1 hay nhiều mails ( vào thừng rác ), khôi phục 1 hay nhiều thư về thư mục ban đầu </p>
    <p> 7. Đánh dấu quan trong, gán nhãn, đánh dấu đọc chưa đọc một hay nhiều mail ở trang home </p>
  <h4>Vấn đề đang gặp phải</h4>
    <p> 
      Về cơ bản, tất cả các chức năng theo PRD đã hoàn thành ở múc trên 80%, chỉ còn thiếu phần "Replies", "Forward", tuy nhiên hiện tại những thứ cần realtime vần
      đang chưa thực hiện được, hiện tại phải reload thì mới được tuần sau sẽ khắc phục vấn đề này bằng MessingCenter và một số cách khác.
    </p>
    <p>
      Chưa tách data với service ra riêng đang bị dồn hết vào data sẽ tách ra vào tuần sau, src code đang chưa tôi ưu có các folder đang bị dư (tạo ra nhưng chưa dùng) sẽ
      cố gắng sửa lại trong tuần tới.
    </p>
    <p>
      Trong lúc test sẽ cõ những lỗi vặt do chưa handle hết các phần đó, lúc test anh sẽ gặp lỗi "Cannot remove child last..." anh nhấn thử lại là được lỗi này xuất hiện trong thằng
      blazor editors nhưng chưa khắc phục được .   
    </p>


<strong>Tuần 5 + Tuần 6: 10/4/2023 - 22/4/2023</strong>
  <h4>Công việc thực hiện được: </h4>
  <p>Cơ bản tất cả chức năng đã xong, những thứ chưa làm: forward tầm 50%, fix ui các thứ vặt vặt</p>
  <p>Tuần sau: fix ui và hoàn thành chặng 2</p>
  <p>Account for test: username: admin1@gmail.com password: admin1</p>
  <p>Các accounts khác tương tự ví dụ: admin2@gmail.com password: admin2 |  admin3@gmail.com password: admin3 | ... có tổng cộng 20 users</p>
  <p>ConnectDB trong folder: Helpers/ConnectDB.cs, backup trong folder "backup", mỗi file .json là 1 model</p>
  <p>Đã thay đổi từ Mongo Local => Mongo Cloud</p>


<strong>Tuần 7 + Tuần 8: 24/4/2023 - 6/5/2023</strong>
  <h4>Công việc thực hiện được: </h4>
  <p>Fix các lỗi trong sheet feedback ở tuần trước</p>
  <p>Hoàn thành các chức năng</p>
  <p>Clean code: Thêm comment, tái cấu trúc src code + chức nắng</p>
  <p>Tài khoản dùng để test như ở trên</p>
    
    
  