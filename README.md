Add your files
Create or upload files
Add files using the command line or push an existing Git repository with the following command:

cd existing_repo

git remote add origin https://gitlab.com/minhpche153232/sep490-g41-project.git

git branch -M main

git push -uf origin main

Dưới đây là một số lệnh cơ bản trong Git:

1. git init: Khởi tạo một kho lưu trữ Git mới trong thư mục hiện tại.

2. git clone <url>: Sao chép một kho lưu trữ Git từ một URL (ví dụ: từ GitHub) vào máy tính của bạn.

3. git add <file>: Thêm một tệp đã chỉ định vào vùng chờ (staging area) để chuẩn bị cho việc commit.

4. git add . hoặc git add --all: Thêm tất cả các thay đổi trong thư mục làm việc hiện tại vào vùng chờ.

5. git commit -m "message": Tạo một commit với các thay đổi đã thêm vào vùng chờ. Message là thông điệp mô tả về commit.

6. git status: Hiển thị trạng thái hiện tại của cây làm việc và vùng chờ.

7. git push: Đẩy các commit đã tạo lên repository từ repository local lên repository trên mạng (ví dụ: GitHub).

8. git pull: Lấy các thay đổi từ repository từ repository trên mạng (ví dụ: GitHub) và kết hợp chúng với repository local.

9. git branch: Liệt kê tất cả các nhánh trong repository.

10. git checkout <branch>: Chuyển đổi sang một nhánh khác.

11. git merge <branch>: Hợp nhất các thay đổi từ một nhánh vào nhánh hiện tại.

12. git log: Hiển thị lịch sử các commit.

13. git diff: Hiển thị sự khác biệt giữa các thay đổi trong vùng chờ và cây làm việc.

14. git rm <file>: Xóa một tệp khỏi cây làm việc và vùng chờ.

15. git reset HEAD <file>: Hủy bỏ việc thêm một tệp vào vùng chờ.
