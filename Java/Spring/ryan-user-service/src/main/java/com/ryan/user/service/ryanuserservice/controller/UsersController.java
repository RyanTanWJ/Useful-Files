package com.ryan.user.service.ryanuserservice.controller;

import com.ryan.user.service.ryanuserservice.Exceptions.DuplicateResourcesException;
import com.ryan.user.service.ryanuserservice.Exceptions.ResourceNotFoundException;
import com.ryan.user.service.ryanuserservice.Exceptions.TestException;
import com.ryan.user.service.ryanuserservice.common.Util;
import com.ryan.user.service.ryanuserservice.datastore.document.User;
import com.ryan.user.service.ryanuserservice.datastore.repository.UserRepository;
import com.ryan.user.service.ryanuserservice.model.request.NewUserRequest;
import com.ryan.user.service.ryanuserservice.model.response.*;
import org.bson.types.ObjectId;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.nio.ByteBuffer;
import java.util.*;

@RestController
@RequestMapping("/users")
public class UsersController {
    private UserRepository userRepository;

    public UsersController(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @GetMapping("/all")
    public ResponseEntity getAll() {
        try {
            List<User> usersList = userRepository.findAll();
            if (usersList == null) {
                throw new TestException();
            }
            if (usersList.size() <= 0) {
                throw new ResourceNotFoundException();
            }
            GetAllUsersResponse response = new GetAllUsersResponse(usersList);
            return new ResponseEntity<GetAllUsersResponse>(response, HttpStatus.OK);
        } catch (ResourceNotFoundException exc) {
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.NoUserContent, "No User Content");
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.NO_CONTENT); // TODO: No Content gives no body in response
        } catch (Exception exc) {
            HashMap<String, Object> metadata = new HashMap<String, Object>();
            metadata.put("exception", exc); // TODO: Putting exception includes entire meta data. Solution this.
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.UnknownError, "Internal Server Error", metadata);
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @GetMapping("/{username}")
    public ResponseEntity findByUserName(@PathVariable("username") String username) {
        try {
            List<User> foundUsers = userRepository.findByUserName(username);
            if (foundUsers.size() < 1) {
                throw new ResourceNotFoundException();
            } else if (foundUsers.size() > 1) {
                throw new DuplicateResourcesException();
            }
            GetUserResponse response = new GetUserResponse(foundUsers.get(0));
            return new ResponseEntity<GetUserResponse>(response, HttpStatus.OK);
        } catch (ResourceNotFoundException exc) {
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.UserNotFound, "User Not Found");
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.NOT_FOUND);
        } catch (DuplicateResourcesException exc) {
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.DuplicateUsersFound, "Internal Server Error");
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.INTERNAL_SERVER_ERROR);
        } catch (Exception exc) {
            HashMap<String, Object> metadata = new HashMap<String, Object>();
            metadata.put("exception", exc); // TODO: Putting exception includes entire meta data. Solution this.
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.UnknownError, "Internal Server Error", metadata);
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }

    @PostMapping("/new")
    public ResponseEntity createNewUser(@RequestBody NewUserRequest newUserRequest) {
        try {
            // TODO: When creating user ensure no duplicate user name
            Date now = new Date();
            ByteBuffer buffer = ByteBuffer.allocate(Long.BYTES);
            buffer.putLong(now.getTime());
            String uid = "RTUS-" + Util.bytesToHex(buffer.array());
            User newUser = new User(
                    uid,
                    newUserRequest.getName(),
                    newUserRequest.getUserName(),
                    now.getTime());

            this.userRepository.save(newUser);
            NewUserResponse response = new NewUserResponse(newUser.getUserId(), newUser.getName(), newUser.getUserName());
            return new ResponseEntity<NewUserResponse>(response, HttpStatus.OK);
        } catch (Exception e) {
            HashMap<String, Object> metadata = new HashMap<String, Object>();
            metadata.put("exception", e); // TODO: Putting exception includes entire meta data. Solution this.
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.UnknownError, "Internal Server Error", metadata);
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
    
    @DeleteMapping("/terminate/{userId}")
    public ResponseEntity deleteUserByUserId(@PathVariable("userId") String userId) {
        try {
            List<User> usersDeleted = this.userRepository.deleteByUserId(userId);
            if (usersDeleted.size() < 1) {
                throw new ResourceNotFoundException();
            } else if (usersDeleted.size() > 1) {
                throw new DuplicateResourcesException();
            }
            User deletedUser = usersDeleted.get(0);
            DeleteUserResponse response = new DeleteUserResponse(deletedUser.getUserId(), deletedUser.getName(), deletedUser.getUserName());
            return new ResponseEntity<DeleteUserResponse>(response, HttpStatus.OK);
        } catch (ResourceNotFoundException exc) {
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.UserNotFound, "User Not Found");
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.NOT_FOUND);
        } catch (DuplicateResourcesException exc) {
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.DuplicateUsersFound, "Internal Server Error");
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.INTERNAL_SERVER_ERROR);
        } catch (Exception e) {
            HashMap<String, Object> metadata = new HashMap<String, Object>();
            metadata.put("exception", e.getStackTrace()); // TODO: Putting exception includes entire meta data. Solution this.
            ExceptionResponse excResp = new ExceptionResponse(ExceptionCodes.UnknownError, "Internal Server Error", metadata);
            return new ResponseEntity<ExceptionResponse>(excResp, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}
