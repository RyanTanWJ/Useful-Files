package com.ryan.user.service.ryanuserservice.controller;

import com.ryan.user.service.ryanuserservice.datastore.document.Users;
import com.ryan.user.service.ryanuserservice.datastore.repository.UserRepository;
import com.ryan.user.service.ryanuserservice.model.request.NewUserRequest;
import com.ryan.user.service.ryanuserservice.model.response.NewUserResponse;
import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/users")
public class UsersController {
    private UserRepository userRepository;

    public UsersController(UserRepository userRepository){
        this.userRepository = userRepository;
    }

    @GetMapping("/all")
    public List<Users> getAll() {
        return userRepository.findAll();
    }

    @PostMapping("/new")
    public NewUserResponse createNewUser(@RequestBody NewUserRequest newUserRequest) {
        Users newUser = new Users(
                1,
                newUserRequest.getName(),
                newUserRequest.getUserName(),
                System.currentTimeMillis()/1000L);
        this.userRepository.save(newUser);
        return new NewUserResponse();
    }
}
